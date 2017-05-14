using System;
using Amazon.Runtime;
using Amazon.SQS;
using Glyde.Configuration;
using Glyde.Di;
using NLog;
using NLog.Config;
using NLog.Targets;
using SiteSpeedManager.Agent.Services;

namespace SiteSpeedManager.Agent.Bootstrapping
{
    public class DependencyInjectionBootstrapping : IDependencyInjectionBootstrapper
    {
        public void RegisterServices(IContainerBuilder containerBuilder, IConfigurationService configurationService)
        {
            containerBuilder.For<IAgentStatusService>().Use<AgentStatusService>().AsTransient();

            // these are services that maintain the agent state, we need to register them as singleton to have them persist
            containerBuilder.For<ISiteSpeedProcess>().Use<SiteSpeedProcess>().AsSingleton();
            containerBuilder.For<ISiteSpeedJobQueueListener>().Use<SiteSpeedJobQueueListener>().AsSingleton();

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
            consoleTarget.Layout = @"AGENT: ${date:format=HH\:mm\:ss} ${level} ${message}";

            // Step 4. Define rules            
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, consoleTarget));
            config.LoggingRules.Add(new LoggingRule("Quartz.*", LogLevel.Debug, consoleTarget));

            // Step 5. Activate the configuration
            LogManager.Configuration = config;

            containerBuilder.For<ILogger>().Use(() => LogManager.GetLogger("App")).AsSingleton();
        }
    }
}