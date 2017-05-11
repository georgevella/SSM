using System;
using Glyde.Configuration.Models;

namespace SiteSpeedManager.Agent.Configuration
{
    public class AgentConfiguration : ConfigurationSection
    {
        public int Port { get; set; }
        public string Hostname { get; set; }
        public Guid Id { get; set; }
    }
}