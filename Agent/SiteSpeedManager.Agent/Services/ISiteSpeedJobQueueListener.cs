using System.Collections.Generic;
using System.Threading.Tasks;

namespace SiteSpeedManager.Agent.Services
{
    public interface ISiteSpeedJobQueueListener
    {
        AgentStatus IsRunning { get; }
        Task Start(IEnumerable<string> countryList);
        void Stop();
    }
}