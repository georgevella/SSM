using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glyde.Web.Api.Controllers;
using Glyde.Web.Api.Controllers.Results;
using Quartz;
using Quartz.Impl.Matchers;
using SiteSpeedController.Master.Jobs;
using SiteSpeedController.Master.Resources.V1;

namespace SiteSpeedController.Master.Controllers.V1
{
    public class SiteSpeedJobController : ApiController<SiteSpeedJobResource, string>
    {
        private readonly IScheduler _scheduler;

        public SiteSpeedJobController(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public override async Task<IEnumerable<SiteSpeedJobResource>> GetAll()
        {
            var list = new List<SiteSpeedJobResource>();

            var jobGroups = await _scheduler.GetJobGroupNames();
            // IList<string> triggerGroups = scheduler.GetTriggerGroupNames();

            foreach (string group in jobGroups)
            {
                var groupMatcher = GroupMatcher<JobKey>.GroupContains(group);
                var jobKeys = await _scheduler.GetJobKeys(groupMatcher);
                foreach (var jobKey in jobKeys)
                {
                    var detail = await _scheduler.GetJobDetail(jobKey);
                    var triggers = await _scheduler.GetTriggersOfJob(jobKey);
                    var trigger = triggers.First();

                    list.Add(new SiteSpeedJobResource()
                    {
                        Id = $"{detail.Key.Name}-{detail.Key.Group}",
                        Site = detail.Key.Group,
                        Path = detail.Key.Name,
                        Crontab = "--",
                        NextExecutionTime = trigger.GetNextFireTimeUtc()?.DateTime ?? DateTime.MinValue
                    });


                    //foreach (ITrigger trigger in triggers)
                    //{
                    //    Console.WriteLine(group);
                    //    Console.WriteLine(jobKey.Name);
                    //    Console.WriteLine(detail.Description);
                    //    Console.WriteLine(trigger.Key.Name);
                    //    Console.WriteLine(trigger.Key.Group);
                    //    Console.WriteLine(trigger.GetType().Name);
                    //    Console.WriteLine(scheduler.GetTriggerState(trigger.Key));
                    //    DateTimeOffset? nextFireTime = trigger.GetNextFireTimeUtc();
                    //    if (nextFireTime.HasValue)
                    //    {
                    //        Console.WriteLine(nextFireTime.Value.LocalDateTime.ToString());
                    //    }

                    //    DateTimeOffset? previousFireTime = trigger.GetPreviousFireTimeUtc();
                    //    if (previousFireTime.HasValue)
                    //    {
                    //        Console.WriteLine(previousFireTime.Value.LocalDateTime.ToString());
                    //    }
                    //}
                }
            }

            return list;
        }

        public override async Task<CreateResourceResult<string>> Create(SiteSpeedJobResource resource)
        {
            var id = $"{resource.Site}-{resource.Path}";

            IJobDetail job = JobBuilder.Create<SiteSpeedJob>()
                .SetJobData(new JobDataMap()
                {
                    { "site", resource.Site },
                    { "path", resource.Path },
                })
                .WithIdentity($"{resource.Path}", $"{resource.Site}")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity($"{resource.Path}", $"{resource.Site}")
                .WithCronSchedule(resource.Crontab)
                .Build();

            var dateTimeOffset = await _scheduler.ScheduleJob(job, trigger);
            resource.NextExecutionTime = dateTimeOffset.DateTime;

            if (!_scheduler.IsStarted)
                await _scheduler.Start();

            return new CreateResourceResult<string>(true, id);
        }
    }
}