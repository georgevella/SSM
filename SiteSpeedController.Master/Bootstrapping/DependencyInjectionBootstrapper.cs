using System;
using System.Linq;
using System.Reflection;
using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SQS;
using Amazon.SQS.Model;
using Glyde.Configuration;
using Glyde.Di;
using Microsoft.AspNetCore.Server.Kestrel.Internal.Networking;
using NLog;
using NLog.Config;
using NLog.Targets;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using SimpleInjector;
using SiteSpeedController.Master.Abstractions;
using SiteSpeedController.Master.Data;
using SiteSpeedController.Master.Services.Jobs;
using SiteSpeedController.Master.Services.Transport;
using SiteSpeedManager.Models.SiteSpeed;

namespace SiteSpeedController.Master.Bootstrapping
{
    public class DependencyInjectionBootstrapper : IDependencyInjectionBootstrapper
    {
        public void RegisterServices(IContainerBuilder containerBuilder, IConfigurationService configurationService)
        {

            // services
            containerBuilder.For<IMessageFactory<SiteSpeedJobDetails>>()
                .Use<MessageFactory<SiteSpeedJobDetails>>()
                .AsSingleton();
            containerBuilder.For<IMessageSerializer<SiteSpeedJobDetails>>()
                .Use<MessageSerializer<SiteSpeedJobDetails>>()
                .AsSingleton();

            containerBuilder.For<ISiteSpeedJobBuilder>().Use<SiteSpeedJobBuilder>().AsTransient();


            // db context
            containerBuilder.For<DataContext>().Use<DataContext>().AsScoped();

            // setup quartz
            ISchedulerFactory sf = new StdSchedulerFactory();
            containerBuilder.For<ISchedulerFactory>().Use(sf);
            containerBuilder.For<IJobFactory>().Use<SimpleInjectorJobFactory>().AsSingleton();
            containerBuilder.For<IScheduler>().Use<SchedulerAbsraction>().AsSingleton();

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
            consoleTarget.Layout = @"APP: ${date:format=HH\:mm\:ss} ${level} ${message}";

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