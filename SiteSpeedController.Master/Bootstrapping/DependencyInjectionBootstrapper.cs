using Glyde.Configuration;
using Glyde.Di;
using SiteSpeedController.Master.Data;

namespace SiteSpeedController.Master.Bootstrapping
{
    public class DependencyInjectionBootstrapper : IDependencyInjectionBootstrapper
    {
        public void RegisterServices(IContainerBuilder containerBuilder, IConfigurationService configurationService)
        {
            containerBuilder.For<DataContext>().Use<DataContext>().AsScoped();
        }
    }
}