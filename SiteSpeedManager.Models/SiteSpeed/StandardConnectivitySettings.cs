namespace SiteSpeedManager.Models.SiteSpeed
{
    public class StandardConnectivitySettings : IConnectivitySettings
    {
        public ConnectivityProfile Profile { get; set; }

        public StandardConnectivitySettings(ConnectivityProfile profile)
        {
            Profile = profile;
        }
    }
}