namespace SiteSpeedManager.Models.SiteSpeed
{
    public class CustomConnectivitySettings : IConnectivitySettings
    {
        public ConnectivityProfile Profile { get; set; }

        // ReSharper disable once InconsistentNaming
        public int downstreamKbps { get; set; }

        // ReSharper disable once InconsistentNaming
        public int upstreamKbps { get; set; }

        public CustomConnectivitySettings(int downstream, int upstream)
        {
            downstreamKbps = downstream;
            upstreamKbps = upstream;
        }
    }
}