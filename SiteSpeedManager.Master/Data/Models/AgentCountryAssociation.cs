using System.ComponentModel.DataAnnotations.Schema;
using SiteSpeedManager.Master.Data.Contracts;

namespace SiteSpeedManager.Master.Data.Models
{
    [Table("agentCountries")]
    public class AgentCountryAssociation : ICountryAssociation
    {
        [Column("agentId")]
        public int AgentId { get; set; }
        public AgentDao Agent { get; set; }

        [Column("countryId")]
        public string CountryId { get; set; }
        public CountryDao Country { get; set; }

        int ICountryAssociation.OwnerId
        {
            get { return AgentId; }
            set { AgentId = value; }
        }
    }
}