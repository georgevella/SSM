namespace SiteSpeedManager.Models.SiteSpeed
{
    public class BrowserTimeSettings
    {
        public BrowserType Browser { get; set; }

        public int Iterations { get; set; }

        public IConnectivitySettings Connectivity { get; set; }

        public string ViewPort { get; set; }

        public string UserAgent { get; set; }

        public bool SpeedIndex { get; set; }

        public bool Video { get; set; }
    }
}