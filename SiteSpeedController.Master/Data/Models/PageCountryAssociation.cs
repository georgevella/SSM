using System.ComponentModel.DataAnnotations.Schema;

namespace SiteSpeedController.Master.Data.Models
{
    [Table("pageCountries")]
    public class PageCountryAssociation
    {
        [Column("pageId")]
        public int PageId { get; set; }
        public PageDao Page { get; set; }

        [Column("countryId")]
        public string CountryId { get; set; }
        public CountryDao Country { get; set; }
    }
}