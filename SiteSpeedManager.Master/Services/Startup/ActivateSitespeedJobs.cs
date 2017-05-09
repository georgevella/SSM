using System;
using System.Threading.Tasks;
using Glyde.ApplicationSupport.ApplicationStartup;
using Microsoft.EntityFrameworkCore;
using SiteSpeedManager.Master.Data;
using SiteSpeedManager.Master.Services.Jobs;
using SiteSpeedManager.Models.SiteSpeed;

namespace SiteSpeedManager.Master.Services.Startup
{
    public class ActivateSitespeedJobs : IRunOnStartup
    {
        private readonly DataContext _dataContext;
        private readonly ISiteSpeedJobBuilder _siteSpeedJobBuilder;

        public ActivateSitespeedJobs(DataContext dataContext, ISiteSpeedJobBuilder siteSpeedJobBuilder)
        {
            _dataContext = dataContext;
            _siteSpeedJobBuilder = siteSpeedJobBuilder;
        }
        public async Task<IStartupServiceResult> Run()
        {
            var sites = _dataContext.Sites.Include(dao => dao.Pages);
            foreach (var site in sites)
            {
                foreach (var sitePage in site.Pages)
                {
                    var conf = new SiteSpeedSettings()
                    {
                        BrowserTime = new BrowserTimeSettings()
                        {
                            Browser = BrowserType.Chrome,
                            Connectivity = new StandardConnectivitySettings(ConnectivityProfile.Fast3G),
                            SpeedIndex = true
                        },
                        Mobile = true
                    };

                    await _siteSpeedJobBuilder.RegisterJob("uk", new Uri(site.Domain), sitePage.Path, conf);
                }
            }

            return new SuccessfulStartupServiceResult();
        }
    }
}