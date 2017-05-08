using System;
using System.Threading.Tasks;
using NLog;
using Quartz;
using SiteSpeedController.Master.Jobs;
using SiteSpeedManager.Models.SiteSpeed;

namespace SiteSpeedController.Master.Services.Jobs
{
    public interface ISiteSpeedJobBuilder
    {
        Task RegisterJob(string countryId, Uri domain, string path, SiteSpeedSettings settings);
    }

    internal class SiteSpeedJobBuilder : ISiteSpeedJobBuilder
    {
        private readonly IScheduler _scheduler;
        private readonly ILogger _log;

        public SiteSpeedJobBuilder(IScheduler scheduler, ILogger log)
        {
            _scheduler = scheduler;
            _log = log;
        }
        public async Task RegisterJob(string countryId, Uri domain, string path, SiteSpeedSettings settings)
        {
            if (!_scheduler.IsStarted)
            {
                _log.Info("Scheduler was not started, starting now ...");
                await _scheduler.Start();
            }

            _log.Info($"Registering job for [{domain}][{path}] in country [{countryId}]");

            var job = JobBuilder.Create<SiteSpeedJob>()
                .SetJobData(new JobDataMap()
                {
                    { SiteSpeedJobDataKeys.Domain, domain },
                    { SiteSpeedJobDataKeys.Path, path },
                    { SiteSpeedJobDataKeys.Settings, settings },
                    { SiteSpeedJobDataKeys.Country, countryId }
                })
                .WithIdentity(path, domain.ToString())
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity(path, domain.ToString())
                .WithSimpleSchedule(builder => builder.RepeatForever().WithIntervalInMinutes(10))
                .StartNow()
                .Build();

            var dateTimeOffset = await _scheduler.ScheduleJob(job, trigger);
            _log.Info($"Job registered, starting @ [{dateTimeOffset.DateTime}]");
        }
    }
}