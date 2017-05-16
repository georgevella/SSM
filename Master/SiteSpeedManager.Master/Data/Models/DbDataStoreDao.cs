using System.ComponentModel.DataAnnotations.Schema;

namespace SiteSpeedManager.Master.Data.Models
{
    public abstract class DbDataStoreDao : DataStoreDao
    {
        [Column("db-host")]
        public string Host { get; set; }

        [Column("db-port")]
        public int Port { get; set; }

        [Column("db-usesCredentials")]
        public bool HasCredentials { get; set; }

        [Column("db-username")]
        public string Username { get; set; }

        [Column("db-password")]
        public string Password { get; set; }

        [Column("db-isEnabled")]
        public bool IsEnabled { get; set; }
    }
}