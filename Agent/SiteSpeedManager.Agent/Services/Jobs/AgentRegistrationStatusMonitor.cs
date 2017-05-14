using System.Threading.Tasks;
using NLog;
using Quartz;
using SiteSpeedManager.Models.Resources.V1;

namespace SiteSpeedManager.Agent.Services.Jobs
{
    public class AgentRegistrationStatusMonitor : IJob
    {
        private readonly IAgentStatusService _agentStatusService;
        private readonly ISiteSpeedJobQueueListener _queueListener;
        private readonly IScheduler _scheduler;
        private readonly ILogger _logger;

        public AgentRegistrationStatusMonitor(IAgentStatusService agentStatusService, ISiteSpeedJobQueueListener queueListener, IScheduler scheduler, ILogger logger)
        {
            _agentStatusService = agentStatusService;
            _queueListener = queueListener;
            _scheduler = scheduler;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.Trace("AgentRegistrationStatusMonitor::Execute() >>");
            var agentInfo = await _agentStatusService.GetAgentInformation();

            if (agentInfo.Status.IsAuthorized())
            {
                _logger.Info("Agent registration accepted, starting queue listener and agent status monitor");

                // start sitespeedjob listener
                await _queueListener.Start(agentInfo.Countries);


                // start agent status monitor
                _logger.Trace("Scheduling [AgentStatusMonitor] job");
                var job = JobBuilder.Create<AgentStatusMonitor>()
                    .WithIdentity("AgentStatusMonitor", "Tasks")
                    .Build();

                var trigger = TriggerBuilder.Create()
                    .WithIdentity("AgentStatusMonitor", "Tasks")
                    .WithSimpleSchedule(builder => builder.RepeatForever().WithIntervalInSeconds(10))
                    .StartNow()
                    .Build();

                var dateTimeOffset = await _scheduler.ScheduleJob(job, trigger);
            }
            _logger.Trace("AgentRegistrationStatusMonitor::Execute() <<");
        }
    }
}