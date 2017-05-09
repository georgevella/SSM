using Glyde.Configuration;
using Glyde.Di;
using SiteSpeedManager.Agent.Core;

namespace SiteSpeedManager.Agent.Bootstrapping
{
    public class DependencyInjectionBootstrapping : IDependencyInjectionBootstrapper
    {
        public void RegisterServices(IContainerBuilder containerBuilder, IConfigurationService configurationService)
        {
            containerBuilder.For<ISiteSpeedProcess>().Use<SiteSpeedProcess>().AsSingleton();
        }
    }
}