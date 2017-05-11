using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glyde.Web.Api.Controllers;
using Microsoft.EntityFrameworkCore;
using SiteSpeedManager.Master.Data;
using SiteSpeedManager.Master.Data.Models;
using SiteSpeedManager.Models.Resources.V1;

namespace SiteSpeedManager.Master.Controllers.V1
{
    public class AgentsController : ApiController<Agent, Guid>
    {
        private readonly DataContext _dataContext;

        public AgentsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public override async Task<IEnumerable<Agent>> GetAll()
        {
            return await Task.Run<IEnumerable<Agent>>(() =>
                _dataContext.Agents.Where(a => a.IsApproved).Select(dao => new Agent()
                {
                    Id = dao.HostIdentifier,
                    Hostname = dao.Hostname,
                    Port = dao.Port,
                    IsEnabled = !dao.IsDisabled,
                    Countries = dao.Countries.Select(x => x.Country.Id)
                }).ToList()
            );
        }

        public override async Task<Agent> Get(Guid id)
        {
            var result = await Task.Run(() => _dataContext.Agents
                .Where(a => a.IsApproved && a.HostIdentifier == id)
                .Select(dao => new Agent()
                {
                    Id = dao.HostIdentifier,
                    Hostname = dao.Hostname,
                    Port = dao.Port,
                    IsEnabled = !dao.IsDisabled,
                    Countries = dao.Countries.Select(x => x.Country.Id)
                }).FirstOrDefault());

            return result;
        }

        public override async Task<bool> Update(Guid id, Agent resource)
        {
            var dao = await _dataContext.Agents.Include(x => x.Countries)
                .FirstOrDefaultAsync(a => a.IsApproved && a.HostIdentifier == id);

            dao.Hostname = resource.Hostname;
            dao.IsDisabled = !resource.IsEnabled;
            dao.Port = resource.Port;
            dao.LastUpdated = DateTime.Now;

            // update country list
            var incomingListOfCountries = _dataContext.Countries.Where(c => resource.Countries.Contains(c.Id)).ToDictionary(x => x.Id);

            if (dao.Countries == null)
                dao.Countries = new List<AgentCountryAssociation>();

            var currentListOfCountries = dao.Countries.ToDictionary(x => x.CountryId);

            var newCountries = incomingListOfCountries.Keys.Except(currentListOfCountries.Keys).ToList();
            var deletedCountries = currentListOfCountries.Keys.Except(incomingListOfCountries.Keys).ToList();

            foreach (var deletedCountryKey in deletedCountries)
            {
                dao.Countries.Remove(currentListOfCountries[deletedCountryKey]);
            }

            foreach (var addedCountryKey in newCountries)
            {
                dao.Countries.Add(new AgentCountryAssociation()
                {
                    Country = incomingListOfCountries[addedCountryKey]
                });
            }

            await Task.Run(() => _dataContext.SaveChanges());

            return true;
        }

        //public override async Task<CreateResourceResult<Guid>> Create(Agent resource)
        //{
        //    var httpContext = _httpContextAccessor.HttpContext;
        //    var remoteIpAddress = httpContext.Features.Get<IHttpConnectionFeature>().RemoteIpAddress;

        //    var result = await _dataContext.Agents.AddAsync(new AgentDao()
        //    {
        //        Created = DateTime.Now,
        //        LastUpdated = DateTime.Now,
        //        HostIdentifier = resource.Id,
        //        Url = $"http://{remoteIpAddress}/api/"
        //    });

        //    _dataContext.SaveChanges();

        //    return Created(resource.Id);
        //}
    }
}