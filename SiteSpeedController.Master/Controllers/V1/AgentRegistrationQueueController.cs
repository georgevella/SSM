using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glyde.Web.Api.Controllers;
using Glyde.Web.Api.Controllers.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using SiteSpeedController.Master.Data;
using SiteSpeedController.Master.Data.Models;
using SiteSpeedController.Master.Resources.V1;

namespace SiteSpeedController.Master.Controllers.V1
{
    public class AgentRegistrationQueueController : ApiController<AgentRegistrationQueue, Guid>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _dataContext;

        public AgentRegistrationQueueController(IHttpContextAccessor httpContextAccessor, DataContext dataContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _dataContext = dataContext;
        }

        public override async Task<IEnumerable<AgentRegistrationQueue>> GetAll()
        {
            return await Task.Run(() =>
                _dataContext.Agents
                    .Where(r => !r.IsApproved)
                    .Select(dao => new AgentRegistrationQueue()
                    {
                        Id = dao.HostIdentifier,
                        Hostname = dao.Hostname,
                        Port = dao.Port,
                        Status = AgentRegistrationStatus.Pending
                    }).ToList()
            );
        }

        public override async Task<AgentRegistrationQueue> Get(Guid id)
        {
            return await Task.Run(() =>
                _dataContext.Agents
                    .Where(r => !r.IsApproved && r.HostIdentifier == id)
                    .Select(dao => new AgentRegistrationQueue()
                    {
                        Id = dao.HostIdentifier,
                        Hostname = dao.Hostname,
                        Port = dao.Port,
                        Status = AgentRegistrationStatus.Pending
                    }).FirstOrDefault()
            );
        }

        public override async Task<bool> Update(Guid id, AgentRegistrationQueue resource)
        {
            var agent = await Task.Run( () => _dataContext.Agents.FirstOrDefault(r => !r.IsApproved && r.HostIdentifier == id));

            if (agent == null)
                return false;

            if (!string.IsNullOrEmpty(resource.Hostname))
                agent.Hostname = resource.Hostname;

            if (resource.Port != 0)
                agent.Port = resource.Port;

            agent.IsApproved = resource.Status == AgentRegistrationStatus.Approved;

            await Task.Run( () => _dataContext.SaveChanges());

            return true;
        }

        public override async Task<CreateResourceResult<Guid>> Create(AgentRegistrationQueue resource)
        {
            if (string.IsNullOrEmpty(resource.Hostname))
            {
                var httpContext = _httpContextAccessor.HttpContext;
                resource.Hostname = httpContext.Features.Get<IHttpConnectionFeature>().RemoteIpAddress.ToString();
            }

            var result = await _dataContext.Agents.AddAsync(new AgentDao()
            {
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                HostIdentifier = resource.Id,
                Hostname = resource.Hostname,
                Port = resource.Port,
                IsApproved = false,
                IsDisabled = true
            });

            _dataContext.SaveChanges();

            return Created(resource.Id);
        }
    }
}