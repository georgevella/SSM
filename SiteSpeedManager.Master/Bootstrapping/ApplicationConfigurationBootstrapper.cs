using Glyde.ApplicationSupport.ApplicationConfiguration;
using Glyde.Configuration;
using SiteSpeedManager.Master.Services.Startup;

namespace SiteSpeedManager.Master.Bootstrapping
{
    public class ApplicationConfigurationBootstrapper : IApplicationConfigurationBootstrapper
    {
        public void RegisterApplicationServices(IApplicationConfigurationBuilder applicationConfigurationBuilder,
            IConfigurationService configurationService)
        {
            applicationConfigurationBuilder.RegisterStartupService<ActivateSitespeedJobs>();
        }
    }
}