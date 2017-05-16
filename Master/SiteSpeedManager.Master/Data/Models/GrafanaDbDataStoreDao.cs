using System.ComponentModel.DataAnnotations.Schema;

namespace SiteSpeedManager.Master.Data.Models
{
    public class GrafanaDbDataStoreDao : DbDataStoreDao
    {
        [Column("grf-httpPort")]
        public int HttpPort { get; set; }

        [Column("grf-namespace")]
        public string Namespace { get; set; }
    }
}