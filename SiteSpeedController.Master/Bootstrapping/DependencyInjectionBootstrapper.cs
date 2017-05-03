﻿using Glyde.Configuration;
using Glyde.Di;
using NLog;
using NLog.Config;
using NLog.Targets;
using Quartz;
using Quartz.Impl;
using SiteSpeedController.Master.Data;

namespace SiteSpeedController.Master.Bootstrapping
{
    public class DependencyInjectionBootstrapper : IDependencyInjectionBootstrapper
    {
        public void RegisterServices(IContainerBuilder containerBuilder, IConfigurationService configurationService)
        {
            containerBuilder.For<DataContext>().Use<DataContext>().AsScoped();
            containerBuilder.For<IScheduler>().Use(() =>
            {
                ISchedulerFactory sf = new StdSchedulerFactory();
                return sf.GetScheduler().Result;
            }).AsSingleton();

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

            containerBuilder.For<ILogger>().Use(() => LogManager.GetLogger("App"));
        }
    }
}