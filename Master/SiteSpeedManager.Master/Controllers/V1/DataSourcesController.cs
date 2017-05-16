using System.Threading.Tasks;
using Glyde.Web.Api.Controllers;
using Glyde.Web.Api.Controllers.Results;
using SiteSpeedManager.Models.Resources.V1;

namespace SiteSpeedManager.Master.Controllers.V1
{
    public class DataSourcesController : ApiController<DataSourceResource, string>
    {
        public override Task<CreateResourceResult<string>> Create(DataSourceResource resource)
        {
            return base.Create(resource);
        }
    }
}