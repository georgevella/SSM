namespace SiteSpeedManager.Models.Resources.V1
{
    public class GrafanaDataSourceResource : DbDataSourceResource
    {
        public int HttpPort { get; set; }
        public string Namespace { get; set; }
    }
}