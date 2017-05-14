using Newtonsoft.Json;

namespace SiteSpeedManager.Models.SiteSpeed
{
    public class SiteSpeedSettings
    {
        [JsonProperty("browsertime")]
        public BrowserTimeSettings BrowserTime { get; set; }

        public GraphiteSettings Graphite { get; set; }

        public InfluxDbSettings Influx { get; set; }

        public AmazonS3Settings S3 { get; set; }

        public bool Mobile { get; set; }

        public string FirstParty { get; set; }

        [JsonProperty("resultBaseURL")]
        public string ResultBaseUrl { get; set; }
    }
}