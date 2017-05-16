using Glyde.Web.Api.Controllers;
using SiteSpeedManager.Master.Helpers;
using SiteSpeedManager.Models.Resources.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteSpeedManager.Master.Controllers.V1
{
    public class DataSourceTypesController : ApiController<DataSourceTypeResource, string>
    {
        public override async Task<IEnumerable<DataSourceTypeResource>> GetAll()
        {
            return await Enum.GetValues(typeof(DataSourceType))
                .Cast<DataSourceType>()
                .ToAsyncEnumerable()
                .Select(
                    x => new DataSourceTypeResource()
                    {
                        Id = x.GetSerializableValue(),
                        DisplayName = x.GetDisplayName()
                    }
                )
                .ToList();
        }
    }
}