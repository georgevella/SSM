using System.ComponentModel.DataAnnotations.Schema;
using SiteSpeedController.Master.Data.Contracts;

namespace SiteSpeedController.Master.Data.Models
{
    [Table("siteCountries")]
    public class SiteCountryAssociation : ICountryAssociation
    {
        [Column("siteId")]
        public int SiteId { get; set; }
        public virtual SiteDao Site { get; set; }

        [Column("countryId")]
        public string CountryId { get; set; }
        public virtual CountryDao Country { get; set; }

        int ICountryAssociation.OwnerId
        {
            get { return SiteId; }
            set { SiteId = value; }
        }
    }
}