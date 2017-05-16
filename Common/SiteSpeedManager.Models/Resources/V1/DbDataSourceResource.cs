namespace SiteSpeedManager.Models.Resources.V1
{
    public abstract class DbDataSourceResource : DataSourceResource
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public bool HasCredentials { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool IsDefault { get; set; }
    }
}