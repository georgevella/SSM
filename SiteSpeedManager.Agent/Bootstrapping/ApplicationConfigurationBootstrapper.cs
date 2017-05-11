using Glyde.ApplicationSupport.ApplicationConfiguration;
using Glyde.Configuration;
using SiteSpeedManager.Agent.Services;

namespace SiteSpeedManager.Agent.Bootstrapping
{
    public class ApplicationConfigurationBootstrapper : IApplicationConfigurationBootstrapper
    {
        public void RegisterApplicationServices(IApplicationConfigurationBuilder applicationConfigurationBuilder,
            IConfigurationService configurationService)
        {
            applicationConfigurationBuilder.RegisterStartupService<AgentRegistrationInitiatorService>();
        }
    }
}