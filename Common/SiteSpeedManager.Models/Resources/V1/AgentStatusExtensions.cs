namespace SiteSpeedManager.Models.Resources.V1
{
    public static class AgentStatusExtensions
    {
        private const int AgentEnabledFlag = 0x2;
        private const int AgentAuthorizedFlag = 0x1;

        public static bool IsEnabledFlagSet(this AgentStatus agentStatus)
        {
            return ((int)agentStatus & AgentEnabledFlag) == AgentEnabledFlag;
        }

        public static bool IsAuthorized(this AgentStatus agentStatus)
        {
            return ((int)agentStatus & AgentAuthorizedFlag) == AgentAuthorizedFlag;
        }
    }
}