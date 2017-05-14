using Glyde.ApplicationSupport.ApplicationConfiguration;
using Glyde.Configuration;
using SiteSpeedManager.Agent.Services;
using SiteSpeedManager.Agent.Services.Startup;

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