namespace SiteSpeedController.Master.Data.Models
{
    public class GrafanaDbDataStoreDao : DataStoreDao
    {
        public int HttpPort { get; set; }

        public string Namespace { get; set; }
    }
}