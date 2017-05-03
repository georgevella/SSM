using System.ComponentModel.DataAnnotations.Schema;
using SiteSpeedController.Master.Data.Contracts;

namespace SiteSpeedController.Master.Data.Models
{
    [Table("pageCountries")]
    public class PageCountryAssociation : ICountryAssociation
    {
        [Column("pageId")]
        public int PageId { get; set; }
        public PageDao Page { get; set; }

        [Column("countryId")]
        public string CountryId { get; set; }
        public CountryDao Country { get; set; }

        int ICountryAssociation.OwnerId
        {
            get { return PageId; }
            set { PageId = value; }
        }
    }
}