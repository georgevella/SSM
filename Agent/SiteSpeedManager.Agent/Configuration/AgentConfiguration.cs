using System;
using Glyde.Configuration.Models;
using Newtonsoft.Json;

namespace SiteSpeedManager.Agent.Configuration
{
    public class AgentConfiguration : ConfigurationSection
    {
        public int Port { get; set; }
        public string Hostname { get; set; }
        public Guid Id { get; set; }

        [JsonProperty("sitespeed")]
        public SiteSpeedExecutableSettings SiteSpeed { get; set; }
    }

    public class SiteSpeedExecutableSettings
    {
        [JsonProperty("command")]
        public string Command { get; set; }

        [JsonProperty("preargs")]
        public string PreArguments { get; set; }

        [JsonProperty("postargs")]
        public string PostArguments { get; set; }
    }
}