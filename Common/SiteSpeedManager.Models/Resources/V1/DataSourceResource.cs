using Glyde.Web.Api.Resources;
using Newtonsoft.Json;

namespace SiteSpeedManager.Models.Resources.V1
{
    [Resource("data-sources")]
    [JsonConverter(typeof(DataSourceResourceConverter))]
    public abstract class DataSourceResource : Resource<string>
    {
        public DataSourceType Type { get; set; }
    }
}