using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteSpeedManager.Master.Data.Models
{
    [Table("datastores")]
    public abstract class DataStoreDao
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }

        [Column("type")]
        public DataStoreType Type { get; set; }
    }
}