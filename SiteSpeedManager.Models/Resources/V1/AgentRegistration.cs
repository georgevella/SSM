using System;
using Glyde.Web.Api.Resources;

namespace SiteSpeedManager.Models.Resources.V1
{
    [Resource("agent-registration-queue")]
    public class AgentRegistrationQueue : Resource<Guid>
    {
        public string Hostname { get; set; }

        public int Port { get; set; }

        public AgentRegistrationStatus Status { get; set; }
    }

    public enum AgentRegistrationStatus
    {
        Pending,
        Approved
    }
}