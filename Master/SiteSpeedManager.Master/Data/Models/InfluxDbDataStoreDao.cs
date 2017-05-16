using System.ComponentModel.DataAnnotations.Schema;

namespace SiteSpeedManager.Master.Data.Models
{
    public class InfluxDbDataStoreDao : DbDataStoreDao
    {
        [Column("inf-database")]
        public string Database { get; set; }
    }
}