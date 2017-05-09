using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glyde.Web.Api.Controllers;
using Glyde.Web.Api.Controllers.Results;
using Microsoft.EntityFrameworkCore;
using SiteSpeedManager.Master.Data;
using SiteSpeedManager.Master.Data.Extensions;
using SiteSpeedManager.Master.Data.Models;
using SiteSpeedManager.Master.Resources.V1;

namespace SiteSpeedManager.Master.Controllers.V1
{
    public class SitesController : ApiController<SiteResource, int>
    {
        private readonly DataContext _dataContext;

        public SitesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public override async Task<IEnumerable<SiteResource>> GetAll()
        {
            return await Task.Run(() => _dataContext.Sites
                .Include(x => x.Countries)
                .Include("Pages.Countries")
                .Include(x => x.Pages)
                .ToList().Select(x => new SiteResource()
                {
                    Countries = x.Countries.Select(c => c.CountryId).ToList(),
                    Id = x.Id,
                    IsEnabled = x.IsEnabled,
                    Domain = x.Domain,
                    Pages = x.Pages.Select(p => new PageResource()
                    {
                        IsEnabled = p.IsEnabled,
                        Alias = p.Alias,
                        Path = p.Path,
                        Countries = (p.OverridesSiteCountryList) ? p.Countries.Select(c => c.CountryId).ToList() : new List<string>(),
                    }).ToList()
                }).ToList());
        }

        public override async Task<CreateResourceResult<int>> Create(SiteResource resource)
        {
            if (_dataContext.Sites.Any(s => s.Domain.Equals(resource.Domain)))
                return AlreadyExists();

            var siteDao = new SiteDao();

            MapSiteResourceToDao(resource, siteDao);

            var entityDetail = await _dataContext.Sites.AddAsync(siteDao);
            await _dataContext.SaveChangesAsync();

            return Created(entityDetail.Entity.Id);
        }

        public override async Task<bool> Update(int id, SiteResource resource)
        {
            var siteDao = _dataContext.Sites
                .Include(x => x.Countries)
                .Include(x => x.Pages)
                .Include("Pages.Countries")
                .Include(x => x.PerformanceProfiles)
                .FirstOrDefault(x => x.Id == id);

            if (siteDao == null)
                return false;

            MapSiteResourceToDao(resource, siteDao);

            var entityDetail = _dataContext.Sites.Update(siteDao);
            await _dataContext.SaveChangesAsync();

            return true;
        }

        private void MapPageResourceToDao(PageResource p, PageDao dao)
        {
            dao.IsEnabled = p.IsEnabled;
            dao.Path = p.Path;
            dao.Alias = p.Alias;

            dao.MaintainCountryList(_dataContext, p.Countries);
            dao.OverridesSiteCountryList = p.Countries.Any();
        }

        private void MapSiteResourceToDao(SiteResource resource, SiteDao siteDao)
        {
            if (resource.Pages == null)
                resource.Pages = new List<PageResource>();

            siteDao.Domain = resource.Domain;
            siteDao.IsEnabled = resource.IsEnabled;

            siteDao.MaintainCountryList(_dataContext, resource.Countries);

            var incomingPaths = resource.Pages.ToDictionary(p => p.Path);
            var currentPaths = siteDao.Pages.ToDictionary(p => p.Path);

            var newPaths = incomingPaths.Keys.Except(currentPaths.Keys).ToList();
            var deletedPaths = currentPaths.Keys.Except(incomingPaths.Keys).ToList();
            var commonPaths = currentPaths.Keys.Intersect(incomingPaths.Keys).ToList();

            foreach (var p in newPaths)
            {
                var dao = new PageDao();
                MapPageResourceToDao(incomingPaths[p], dao);
                siteDao.Pages.Add(dao);
            }

            foreach (var p in deletedPaths)
            {
                siteDao.Pages.Remove(currentPaths[p]);
            }

            foreach (var p in commonPaths)
            {
                var currentPage = currentPaths[p];
                var pageresource = incomingPaths[p];
                MapPageResourceToDao(pageresource, currentPage);

            }
        }
    }
}