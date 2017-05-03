using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteSpeedController.Master.Data.Models
{
    [Table("datastores")]
    public class DataStoreDao
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool IsDefault { get; set; }
    }
}