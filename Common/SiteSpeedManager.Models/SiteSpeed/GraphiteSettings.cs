namespace SiteSpeedManager.Models.SiteSpeed
{
    public class GraphiteSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Auth { get; set; }
        public int HttpPort { get; set; }
        public string Namespace { get; set; }
        public string IncludeQueryParams { get; set; }
    }
}