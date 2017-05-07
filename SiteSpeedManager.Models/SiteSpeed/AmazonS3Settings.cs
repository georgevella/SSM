namespace SiteSpeedManager.Models.SiteSpeed
{
    public class AmazonS3Settings
    {
        public string Key { get; set; }
        public string Secret { get; set; }
        public string Bucketname { get; set; }
        public string Path { get; set; }
        public string Region { get; set; }
        public string RemoveLocalResult { get; set; }
    }
}