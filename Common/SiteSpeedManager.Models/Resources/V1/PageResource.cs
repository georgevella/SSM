using System.Collections.Generic;

namespace SiteSpeedManager.Models.Resources.V1
{
    public class PageResource
    {
        public string Path { get; set; }

        public string Alias { get; set; }

        public string CronSchedule { get; set; }

        public bool IsEnabled { get; set; }

        public List<string> Countries { get; set; } = new List<string>();
    }
}