using System.Collections.Generic;
using SiteSpeedManager.Master.Data.Models;

namespace SiteSpeedManager.Master.Data.Contracts
{
    public interface ICountryListContainer<TCountryAssociation>
    {
        int Id { get; set; }

        List<TCountryAssociation> Countries { get; set; }
    }

    public interface ICountryAssociation
    {
        string CountryId { get; set; }
        CountryDao Country { get; set; }

        int OwnerId { get; set; }
    }
}