using System.ComponentModel.DataAnnotations.Schema;

namespace SiteSpeedManager.Master.Data.Models
{
    public class S3DataStoreDao : DataStoreDao
    {
        [Column("aws-accesskey")]
        public string Key { get; set; }

        [Column("aws-secretkey")]
        public string Secret { get; set; }

        [Column("aws-s3-bucket")]
        public string BucketName { get; set; }

        [Column("aws-s3-path")]
        public string Path { get; set; }

        [Column("aws-region")]
        public string Region { get; set; }
    }
}