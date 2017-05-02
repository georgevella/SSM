using Glyde.Web.Api.Resources;

namespace SiteSpeedController.Master.Resources.V1
{
    [Resource("countries")]
    public class Country : Resource<string>
    {
        public string DisplayName { get; set; }
    }
}