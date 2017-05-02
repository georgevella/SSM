using System;
using System.Collections.Generic;
using Glyde.Web.Api.Resources;

namespace SiteSpeedController.Master.Resources.V1
{
    [Resource("agents")]
    public class Agent : Resource<Guid>
    {
        public string Hostname { get; set; }

        public int Port { get; set; }

        public bool IsEnabled { get; set; }

        public IEnumerable<string> Countries { get; set; } = new string[] {};
    }
}