using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glyde.Web.Api.Controllers;
using Glyde.Web.Api.Controllers.Results;
using Microsoft.AspNetCore.Http.Features;
using SiteSpeedController.Master.Data;
using SiteSpeedController.Master.Data.Models;
using SiteSpeedController.Master.Resources.V1;

namespace SiteSpeedController.Master.Controllers.V1
{
    public class CountriesController : ApiController<Country, string>
    {
        private readonly DataContext _dataContext;

        public CountriesController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public override async Task<IEnumerable<Country>> GetAll()
        {
            return await Task.Run(() =>
                _dataContext.Countries
                    .Select(dao => new Country()
                    {
                        Id = dao.Id,
                        DisplayName = dao.Name,
                        IsEnabled = dao.IsEnabled
                    }).ToList()
            );
        }

        public override async Task<Country> Get(string id)
        {
            var dao = await _dataContext.Countries
                .FindAsync(id);

            return new Country()
            {
                Id = dao.Id,
                DisplayName = dao.Name,
                IsEnabled = dao.IsEnabled
            };
        }

        public override async Task<bool> Update(string id, Country resource)
        {
            var dao = await _dataContext.Countries.FindAsync(id);

            if (dao == null)
                return false;

            dao.Name = resource.DisplayName;
            dao.IsEnabled = resource.IsEnabled;

            _dataContext.Countries.Update(dao);

            await Task.Run(() => _dataContext.SaveChanges());

            return true;
        }

        public override async Task<CreateResourceResult<string>> Create(Country resource)
        {
            if (_dataContext.Countries.Any(c => c.Id == resource.Id || c.Name == resource.DisplayName))
                return AlreadyExists();

            var result = await _dataContext.Countries.AddAsync(new CountryDao()
            {
                Id = resource.Id,
                Name = resource.DisplayName,
                IsEnabled = resource.IsEnabled
            });

            _dataContext.SaveChanges();

            return Created(resource.Id);
        }
    }
}