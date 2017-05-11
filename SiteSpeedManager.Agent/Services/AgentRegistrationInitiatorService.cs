using System;
using System.Threading.Tasks;
using Glyde.ApplicationSupport.ApplicationStartup;
using Glyde.Configuration;
using Glyde.Web.Api.Client;
using Glyde.Web.Api.Exceptions;
using NLog;
using SiteSpeedManager.Agent.Configuration;
using SiteSpeedManager.Models.Resources.V1;

namespace SiteSpeedManager.Agent.Services
{
    public class AgentRegistrationInitiatorService : IRunOnStartup
    {


        private readonly IConfigurationService _configurationService;
        private readonly ILogger _logger;
        private readonly IApiClient<AgentRegistrationQueue> _apiClient;

        public AgentRegistrationInitiatorService(IApiClientFactory apiClientFactory, IConfigurationService configurationService, ILogger logger)
        {
            _configurationService = configurationService;
            _logger = logger;
            _apiClient = apiClientFactory.GetClientFor<AgentRegistrationQueue>();
        }

        public async Task<IStartupServiceResult> Run()
        {
            _logger.Trace("AgentRegistrationInitiatorService::Run() >>");
            var agentConfiguration = _configurationService.Get<AgentConfiguration>();

            // TODO check agents list to check if we are already registered and approved

            try
            {
                _logger.Info("Starting agent registration process");
                await _apiClient.Create(new AgentRegistrationQueue()
                {
                    Id = agentConfiguration.Id,
                    Hostname = agentConfiguration.Hostname,
                    Port = agentConfiguration.Port
                });
            }
            catch (ResourceExistsException e)
            {
                _logger.Info("Registration is already in queue, checking status");

                await _apiClient.Get(agentConfiguration.Id);

            }

            // TODO: start job to check state of agent registration

            _logger.Trace("AgentRegistrationInitiatorService::Run() <<");
            return new SuccessfulStartupServiceResult();
        }
    }
}