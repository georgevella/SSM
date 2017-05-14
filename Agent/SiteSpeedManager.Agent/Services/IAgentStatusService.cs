using System.Threading.Tasks;

namespace SiteSpeedManager.Agent.Services
{
    public interface IAgentStatusService
    {
        Task<AgentRegistrationStatusType> GetAgentRegistrationStatus();
        Task<bool> TryRegisterAgent();
        Task<Models.Resources.V1.Agent> GetAgentInformation();
        Task<AgentStatus> IsAgentEnabled();
    }
}