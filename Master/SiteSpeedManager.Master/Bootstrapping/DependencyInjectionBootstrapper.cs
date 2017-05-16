using Amazon.Runtime;
using Amazon.SQS;
using AutoMapper;
using Glyde.Configuration;
using Glyde.Di;
using NLog;
using NLog.Config;
using NLog.Targets;
using Quartz;
using Quartz.Spi;
using SiteSpeedManager.Master.Data;
using SiteSpeedManager.Master.Data.Models;
using SiteSpeedManager.Master.Services.Jobs;
using SiteSpeedManager.Master.Services.Mapping;
using SiteSpeedManager.Models.Resources.V1;
using System;

namespace SiteSpeedManager.Master.Bootstrapping
{
    public class DependencyInjectionBootstrapper : IDependencyInjectionBootstrapper
    {
        public void RegisterServices(IContainerBuilder containerBuilder, IConfigurationService configurationService)
        {
            Mapper.Initialize(expression =>
            {
                expression.CreateMap<GrafanaDbDataStoreDao, GrafanaDataSourceResource>();
                expression.CreateMap<InfluxDbDataStoreDao, InfluxDbDataSourceResource>();
                expression.CreateMap<S3DataStoreDao, S3DataSourceResource>();
            });

            // services
            containerBuilder.For<ISiteSpeedJobBuilder>().Use<SiteSpeedJobBuilder>().AsTransient();

            containerBuilder.For<IMapper<AgentDao, Agent>>().Use<AgentMapper>();
            containerBuilder.For<IMapper<Agent, AgentDao>>().Use<AgentDaoMapper>();

            // db context
            containerBuilder.For<DataContext>().Use<DataContext>().AsScoped();

            // build aws credentials
            var accessKey = Environment.GetEnvironmentVariable("AWS_ACCESSKEY");
            var secretKey = Environment.GetEnvironmentVariable("AWS_SECRETKEY");

            if (string.IsNullOrEmpty(accessKey) || string.IsNullOrEmpty(secretKey))
                throw new InvalidOperationException();

            var awsCredentials = new BasicAWSCredentials(accessKey, secretKey);

            // setup aws sqs
            var sqsConfig = new AmazonSQSConfig
            {
                ServiceURL = "https://sqs.eu-west-1.amazonaws.com/"
            };

            var sqsClient = new AmazonSQSClient(awsCredentials, sqsConfig);

            containerBuilder.For<IAmazonSQS>().Use(sqsClient);

            // Step 1. Create configuration object 
            var config = new LoggingConfiguration();

            // Step 2. Create targets and add them to the configuration 
            var consoleTarget = new ColoredConsoleTarget();
            config.AddTarget("console", consoleTarget);
            // Step 3. Set target properties 
            consoleTarget.Layout = @"MASTER: ${date:format=HH\:mm\:ss} ${level} ${message}";

            // Step 4. Define rules
            var rule1 = new LoggingRule("*", LogLevel.Debug, consoleTarget);
            config.LoggingRules.Add(rule1);

            // Step 5. Activate the configuration
            LogManager.Configuration = config;

            containerBuilder.For<ILogger>().Use(() => LogManager.GetLogger("App")).AsSingleton();
        }
    }


    public class SimpleInjectorJobFactory : IJobFactory
    {
        private readonly IServiceProvider _container;

        public SimpleInjectorJobFactory(IServiceProvider container)
        {
            _container = container;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return (IJob)_container.GetService(bundle.JobDetail.JobType);
        }

        public void ReturnJob(IJob job)
        {

        }
    }
}