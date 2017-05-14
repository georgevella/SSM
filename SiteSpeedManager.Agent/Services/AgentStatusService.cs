using System.Threading.Tasks;
using Glyde.Configuration;
using Glyde.Web.Api.Client;
using Glyde.Web.Api.Exceptions;
using NLog;
using SiteSpeedManager.Agent.Configuration;
using SiteSpeedManager.Models.Resources.V1;

namespace SiteSpeedManager.Agent.Services
{
    public class AgentStatusService : IAgentStatusService
    {
        private readonly ILogger _logger;
        private readonly AgentConfiguration _agentConfiguration;
        private readonly IApiClient<AgentRegistrationQueue> _registrationQueueApiClient;
        private readonly IApiClient<Models.Resources.V1.Agent> _agentApiClient;

        public AgentStatusService(IApiClientFactory apiClientFactory, IConfigurationService configurationService,
            ILogger logger)
        {
            _registrationQueueApiClient = apiClientFactory.GetClientFor<AgentRegistrationQueue>();
            _agentApiClient = apiClientFactory.GetClientFor<Models.Resources.V1.Agent>();

            _agentConfiguration = configurationService.Get<AgentConfiguration>();
            _logger = logger;
        }

        public async Task<AgentRegistrationStatusType> GetAgentRegistrationStatus()
        {
            try
            {
                var queueItem = await _agentApiClient.Get(_agentConfiguration.Id);

                return (queueItem.Status.IsAuthorized())
                    ? AgentRegistrationStatusType.Accepted
                    : AgentRegistrationStatusType.Pending;
            }
            catch (ResourceDoesNotExistException)
            {
                return AgentRegistrationStatusType.NotAvailable;
            }
        }

        public async Task<bool> TryRegisterAgent()
        {
            try
            {
                _logger.Info("Starting agent registration process");
                await _registrationQueueApiClient.Create(new AgentRegistrationQueue()
                {
                    Id = _agentConfiguration.Id,
                    Hostname = _agentConfiguration.Hostname,
                    Port = _agentConfiguration.Port
                });

                return true;
            }
            catch (ResourceExistsException)
            {
                return false;
            }
        }

        public async Task<Models.Resources.V1.Agent> GetAgentInformation()
        {
            var queueItem = await _agentApiClient.Get(_agentConfiguration.Id);

            return queueItem;
        }

        public async Task<AgentStatus> IsAgentEnabled()
        {
            try
            {
                var queueItem = await _agentApiClient.Get(_agentConfiguration.Id);

                return (queueItem.Status.IsEnabledFlagSet())
                    ? AgentStatus.Enabled
                    : AgentStatus.Disabled;
            }
            catch (ResourceDoesNotExistException)
            {
                return AgentStatus.Disabled;
            }
        }
    }
}