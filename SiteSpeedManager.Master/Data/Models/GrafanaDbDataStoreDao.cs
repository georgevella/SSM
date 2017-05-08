using System.ComponentModel.DataAnnotations.Schema;

namespace SiteSpeedController.Master.Data.Models
{
    public class GrafanaDbDataStoreDao : DataStoreDao
    {
        [Column("httpPort")]
        public int HttpPort { get; set; }

        [Column("namespace")]
        public string Namespace { get; set; }
    }
}