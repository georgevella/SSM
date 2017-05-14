using System.Threading.Tasks;
using NLog;
using Quartz;
using SiteSpeedManager.Models.Resources.V1;

namespace SiteSpeedManager.Agent.Services.Jobs
{
    public class AgentStatusMonitor : IJob
    {
        private readonly IAgentStatusService _agentStatusService;
        private readonly ISiteSpeedJobQueueListener _queueListener;
        private readonly IScheduler _scheduler;
        private readonly ILogger _logger;

        public AgentStatusMonitor(
            IAgentStatusService agentStatusService,
            ISiteSpeedJobQueueListener queueListener,
            IScheduler scheduler,
            ILogger logger)
        {
            _agentStatusService = agentStatusService;
            _queueListener = queueListener;
            _scheduler = scheduler;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.Trace("AgentStatusMonitor::Execute() >>");

            var agentInfo = await _agentStatusService.GetAgentInformation();

            var agentStatus = agentInfo.Status.IsEnabledFlagSet() ? AgentStatus.Enabled : AgentStatus.Disabled;

            if (agentStatus != _queueListener.IsRunning)
            {
                _logger.Info($"Agent changed state from [{_queueListener.IsRunning}] to [{agentStatus}] ");
                if (agentStatus == AgentStatus.Enabled)
                    await _queueListener.Start(agentInfo.Countries);
                else
                    _queueListener.Stop();
            }

            _logger.Trace("AgentStatusMonitor::Execute() <<");

        }
    }
}