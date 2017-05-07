namespace SiteSpeedManager.Models.SiteSpeed
{
    public class SiteSpeedSettings
    {
        public BrowserTimeSettings BrowserTime { get; set; }

        public GraphiteSettings Graphite { get; set; }

        public InfluxDbSettings Influx { get; set; }

        public AmazonS3Settings S3 { get; set; }

        public bool Mobile { get; set; }

        public string FirstParty { get; set; }

        // ReSharper disable once InconsistentNaming
        public string ResultBaseURL { get; set; }
    }
}