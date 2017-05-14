using System;
using System.Linq;
using System.Threading.Tasks;
using Glyde.ApplicationSupport.ApplicationStartup;
using Microsoft.EntityFrameworkCore;
using NLog;
using SiteSpeedManager.Master.Data;
using SiteSpeedManager.Master.Services.Jobs;
using SiteSpeedManager.Models.SiteSpeed;

namespace SiteSpeedManager.Master.Services.Startup
{
    /// <summary>
    ///     Service triggered on application startup to find all active site speed jobs and schedule them.
    /// </summary>
    public class ActivateSitespeedJobs : IRunOnStartup
    {
        private readonly DataContext _dataContext;
        private readonly ISiteSpeedJobBuilder _siteSpeedJobBuilder;
        private readonly ILogger _logger;

        public ActivateSitespeedJobs(DataContext dataContext, ISiteSpeedJobBuilder siteSpeedJobBuilder, ILogger logger)
        {
            _dataContext = dataContext;
            _siteSpeedJobBuilder = siteSpeedJobBuilder;
            _logger = logger;
        }

        public async Task<IStartupServiceResult> Run()
        {
            _logger.Trace("ActivateSitespeedJobs::Run() >>");

            var sites = _dataContext.Sites
                .Include(dao => dao.Pages)
                .ThenInclude(x => x.Countries)
                .Include(dao => dao.Countries);
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
                            SpeedIndex = true,
                            Video = true,
                            Iterations = 1
                        },
                        Mobile = true
                    };

                    var countryList = site.Countries.Select(x => x.CountryId);
                    if (sitePage.OverridesSiteCountryList)
                        countryList = sitePage.Countries.Select(x => x.CountryId);

                    _logger.Info($"Activating scheduled job for page [{site.Domain}/{sitePage.Path}] for countries [{string.Join(",", countryList)}");

                    foreach (var country in countryList)
                    {
                        await _siteSpeedJobBuilder.RegisterJob(country, new Uri(site.Domain), sitePage.Path, conf);
                    }
                }
            }

            _logger.Trace("ActivateSitespeedJobs::Run() <<");
            return new SuccessfulStartupServiceResult();
        }
    }
}