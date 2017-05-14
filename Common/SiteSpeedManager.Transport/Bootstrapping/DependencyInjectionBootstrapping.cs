using Glyde.Configuration;
using Glyde.Di;
using SiteSpeedManager.Models.SiteSpeed;

namespace SiteSpeedManager.Transport.Bootstrapping
{
    public class DependencyInjectionBootstrapping : IDependencyInjectionBootstrapper
    {
        public void RegisterServices(IContainerBuilder containerBuilder, IConfigurationService configurationService)
        {
            containerBuilder.For<IMessageFactory<SiteSpeedJobDetails>>()
                .Use<MessageFactory<SiteSpeedJobDetails>>()
                .AsSingleton();
            containerBuilder.For<IMessageSerializer<SiteSpeedJobDetails>>()
                .Use<MessageSerializer<SiteSpeedJobDetails>>()
                .AsSingleton();
        }
    }
}