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

        [Column("type")]
        public DataStoreType Type { get; set; }

        [Column("host")]
        public string Host { get; set; }

        [Column("port")]
        public int Port { get; set; }

        [Column("hasCredentials")]
        public bool HasCredentials { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("isDefault")]
        public bool IsDefault { get; set; }
    }
}