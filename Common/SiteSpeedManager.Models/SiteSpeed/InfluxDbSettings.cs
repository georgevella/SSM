namespace SiteSpeedManager.Models.SiteSpeed
{
    public class InfluxDbSettings
    {
        public string Protocol { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public bool IncludeQueryParams { get; set; }
    }
}