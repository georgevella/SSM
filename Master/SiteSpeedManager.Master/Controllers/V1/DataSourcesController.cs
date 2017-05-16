using AutoMapper;
using Glyde.Web.Api.Controllers;
using Glyde.Web.Api.Controllers.Results;
using SiteSpeedManager.Master.Data;
using SiteSpeedManager.Master.Data.Models;
using SiteSpeedManager.Models.Resources.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteSpeedManager.Master.Controllers.V1
{
    public class DataSourcesController : ApiController<DataSourceResource, string>
    {
        private readonly DataContext _dataContext;

        public DataSourcesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public override async Task<IEnumerable<DataSourceResource>> GetAll()
        {
            var dataStores = _dataContext.DataStores.ToList();

            var result = dataStores.Select<DataStoreDao, DataSourceResource>(s =>
            {
                switch (s.Type)
                {
                    case DataStoreType.GrafanaDb:
                        return Mapper.Map<GrafanaDbDataStoreDao, GrafanaDataSourceResource>((GrafanaDbDataStoreDao)s);
                    case DataStoreType.InfluxDb:
                        return Mapper.Map<InfluxDbDataStoreDao, InfluxDbDataSourceResource>(
                            (InfluxDbDataStoreDao)s);
                    case DataStoreType.S3Bucket:
                        return Mapper.Map<S3DataStoreDao, S3DataSourceResource>(
                            (S3DataStoreDao)s);
                }

                return null;
            }).ToList();

            return result;
        }

        public override async Task<CreateResourceResult<string>> Create(DataSourceResource resource)
        {
            switch (resource.Type)
            {
                case DataSourceType.GrafanaDb:
                    var grafanaDataSource = (GrafanaDataSourceResource)resource;
                    var entity = _dataContext.DataStores.Add(new GrafanaDbDataStoreDao()
                    {
                        Id = resource.Id,
                        HasCredentials = grafanaDataSource.HasCredentials,
                        Type = DataStoreType.GrafanaDb,
                        Host = grafanaDataSource.Host,
                        HttpPort = grafanaDataSource.HttpPort,
                        IsEnabled = resource.IsEnabled,
                        Namespace = grafanaDataSource.Namespace,
                        Password = grafanaDataSource.Password,
                        Username = grafanaDataSource.Username,
                        Port = grafanaDataSource.Port
                    });

                    await _dataContext.SaveChangesAsync();
                    return Created(entity.Entity.Id);
                    break;
            }

            throw new NotImplementedException();
        }
    }
}