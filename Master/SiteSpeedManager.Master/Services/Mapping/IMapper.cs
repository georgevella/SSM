using System;
using System.Linq;
using SiteSpeedManager.Master.Data.Models;
using SiteSpeedManager.Models.Resources.V1;

namespace SiteSpeedManager.Master.Services.Mapping
{
    public interface IMapper<in TSource, TDest>
    {
        TDest Map(TSource source);
        void Map(TSource source, TDest dest);
    }

    public class AgentMapper : IMapper<AgentDao, Agent>
    {
        public Agent Map(AgentDao dao)
        {
            var agent = new Agent
            {
                Id = dao.HostIdentifier,
                Hostname = dao.Hostname,
                Port = dao.Port,
                Countries = dao.Countries.Select(x => x.CountryId),
                Status = (dao.IsApproved)
                    ? (dao.IsDisabled ? AgentStatus.AuthorizedAndDisabled : AgentStatus.AuthorizedAndEnabled)
                    : AgentStatus.Unauthorized
            };


            return agent;
        }

        public void Map(AgentDao source, Agent dest)
        {
            throw new System.NotImplementedException();
        }
    }

    public class AgentDaoMapper : IMapper<Agent, AgentDao>
    {
        public AgentDao Map(Agent source)
        {
            throw new NotSupportedException();
        }

        public void Map(Agent resource, AgentDao dao)
        {
            dao.Hostname = resource.Hostname;
            dao.IsDisabled = !resource.Status.IsEnabledFlagSet();
            dao.IsApproved = resource.Status != AgentStatus.Unauthorized;
            dao.Port = resource.Port;
            dao.LastUpdated = DateTime.Now;
        }
    }
}