using System.ComponentModel.DataAnnotations.Schema;

namespace SiteSpeedController.Master.Data.Models
{
    public class InfluxDbDataStoreDao : DataStoreDao
    {
        [Column("database")]
        public string Database { get; set; }
    }
}