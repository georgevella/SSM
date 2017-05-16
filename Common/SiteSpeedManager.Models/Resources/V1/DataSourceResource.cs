using Glyde.Web.Api.Resources;
using Newtonsoft.Json;

namespace SiteSpeedManager.Models.Resources.V1
{
    [Resource("data-sources")]
    [JsonConverter(typeof(DataSourceResourceConverter))]
    public abstract class DataSourceResource : Resource<string>
    {
        public DataSourceType Type { get; set; }

        public bool IsEnabled { get; set; }
    }

    public class S3DataSourceResource : DataSourceResource
    {
        public string Key { get; set; }

        public string Secret { get; set; }

        public string BucketName { get; set; }

        public string Path { get; set; }

        public string Region { get; set; }
    }
}