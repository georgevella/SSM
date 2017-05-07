using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using Glyde.ApplicationSupport.ApplicationStartup;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Quartz;
using SiteSpeedController.Master.Data;
using SiteSpeedController.Master.Services.Jobs;
using SiteSpeedController.Master.Services.Transport;
using SiteSpeedManager.Models.SiteSpeed;

namespace SiteSpeedController.Master.Services.Startup
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