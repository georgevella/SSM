using System.ComponentModel.DataAnnotations.Schema;

namespace SiteSpeedManager.Master.Data.Models
{
    public class InfluxDbDataStoreDao : DataStoreDao
    {
        [Column("database")]
        public string Database { get; set; }
    }
}