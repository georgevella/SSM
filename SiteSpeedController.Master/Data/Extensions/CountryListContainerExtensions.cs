using System.Collections.Generic;
using System.Linq;
using SiteSpeedController.Master.Data.Contracts;
using SiteSpeedController.Master.Data.Models;

namespace SiteSpeedController.Master.Data.Extensions
{
    public static class CountryListContainerExtensions
    {
        /// <summary>
        ///     Compares two country lists and ensures that the target has an up-to-date country list.
        /// </summary>
        /// <typeparam name="TCountryAssociation"></typeparam>
        /// <param name="countryListContainer">Reference to the entity that will receive the updated country list.</param>
        /// <param name="dataContext"></param>
        /// <param name="incomingCountryIds"></param>
        public static void MaintainCountryList<TCountryAssociation>(
            this ICountryListContainer<TCountryAssociation> countryListContainer,
            DataContext dataContext,
            IEnumerable<string> incomingCountryIds)
            where TCountryAssociation : ICountryAssociation, new()
        {
            var incomingListOfCountries = dataContext.Countries
                .Where(c => incomingCountryIds.Contains(c.Id))
                .ToDictionary(x => x.Id);
            var currentListOfCountries = countryListContainer.Countries.ToDictionary(x => x.CountryId);

            var newCountries = incomingListOfCountries.Keys.Except(currentListOfCountries.Keys).ToList();
            var deletedCountries = currentListOfCountries.Keys.Except(incomingListOfCountries.Keys).ToList();

            foreach (var deletedCountryKey in deletedCountries)
            {
                countryListContainer.Countries.Remove(currentListOfCountries[deletedCountryKey]);
            }

            foreach (var addedCountryKey in newCountries)
            {
                countryListContainer.Countries.Add(new TCountryAssociation()
                {
                    Country = incomingListOfCountries[addedCountryKey],
                    CountryId = addedCountryKey,
                    OwnerId = countryListContainer.Id
                });
            }
        }
    }
}