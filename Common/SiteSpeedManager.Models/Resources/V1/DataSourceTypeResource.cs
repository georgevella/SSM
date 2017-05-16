using Glyde.Web.Api.Resources;

namespace SiteSpeedManager.Models.Resources.V1
{
    [Resource("data-source-types")]
    public class DataSourceTypeResource : Resource<string>
    {
        public string DisplayName { get; set; }
    }
}