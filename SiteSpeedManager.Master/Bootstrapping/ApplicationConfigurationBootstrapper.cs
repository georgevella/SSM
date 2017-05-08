using Glyde.ApplicationSupport.ApplicationConfiguration;
using Glyde.Configuration;
using SiteSpeedController.Master.Services.Startup;

namespace SiteSpeedController.Master.Bootstrapping
{
    public class ApplicationConfigurationBootstrapper : IApplicationConfigurationBootstrapper
    {
        public void RegisterApplicationStartupService(IApplicationConfigurationBuilder applicationConfigurationBuilder,
            IConfigurationService configurationService)
        {
            applicationConfigurationBuilder.RegisterStartupService<ActivateSitespeedJobs>();
        }
    }
}