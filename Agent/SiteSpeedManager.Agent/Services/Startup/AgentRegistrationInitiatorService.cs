using System.Threading.Tasks;
using Glyde.ApplicationSupport.ApplicationStartup;
using NLog;
using Quartz;
using SiteSpeedManager.Agent.Services.Jobs;

namespace SiteSpeedManager.Agent.Services.Startup
{
    public class AgentRegistrationInitiatorService : IRunOnStartup
    {
        private readonly IAgentStatusService _agentRegistrationService;
        private readonly ILogger _logger;
        private readonly IScheduler _scheduler;

        public AgentRegistrationInitiatorService(IAgentStatusService agentRegistrationService, ILogger logger, IScheduler scheduler)
        {
            _agentRegistrationService = agentRegistrationService;
            _logger = logger;
            _scheduler = scheduler;
        }

        public async Task<IStartupServiceResult> Run()
        {
            _logger.Trace("AgentRegistrationInitiatorService::Run() >>");

            // check agents list to check if we are already registered and approved
            var status = await _agentRegistrationService.GetAgentRegistrationStatus();
            switch (status)
            {
                case AgentRegistrationStatusType.NotAvailable:
                    if (await _agentRegistrationService.TryRegisterAgent())
                    {
                        _logger.Info("Registration request sent to master.");
                    }
                    break;

                case AgentRegistrationStatusType.Pending:
                    {
                        _logger.Info("Registration not accepted. Starting job for to monitor status");

                        var job = JobBuilder.Create<AgentRegistrationStatusMonitor>()
                            .WithIdentity("AgentRegistrationStatusCheck", "Tasks")
                            .Build();

                        var trigger = TriggerBuilder.Create()
                            .WithIdentity("AgentRegistrationStatusCheck", "Tasks")
                            .WithSimpleSchedule(builder => builder.RepeatForever().WithIntervalInSeconds(10))
                            .StartNow()
                            .Build();

                        var dateTimeOffset = await _scheduler.ScheduleJob(job, trigger);
                    }

                    break;

                case AgentRegistrationStatusType.Accepted:
                    {
                        _logger.Info("Agent authorized, starting status monitor");

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
                    break;
            }

            // TODO: start job to check state of agent (enabled / disabled)

            _logger.Trace("AgentRegistrationInitiatorService::Run() <<");
            return new SuccessfulStartupServiceResult();
        }
    }
}