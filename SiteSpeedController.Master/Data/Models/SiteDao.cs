using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteSpeedController.Master.Data.Models
{
    [Table("sites")]
    public class SiteDao
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("domain")]
        public string Domain { get; set; }

        public DataStoreDao DataStore { get; set; }
    }
}