using Glyde.Configuration;
using Glyde.Di;
using SiteSpeedController.Agent.Core;

namespace SiteSpeedController.Agent.Bootstrapping
{
    public class DependencyInjectionBootstrapping : IDependencyInjectionBootstrapper
    {
        public void RegisterServices(IContainerBuilder containerBuilder, IConfigurationService configurationService)
        {
            containerBuilder.For<ISiteSpeedProcess>().Use<SiteSpeedProcess>().AsSingleton();
        }
    }
}