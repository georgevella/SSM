using System.ComponentModel.DataAnnotations.Schema;

namespace SiteSpeedController.Master.Data.Models
{
    [Table("agentCountries")]
    public class AgentCountryAssociation
    {
        public int AgentId { get; set; }
        public AgentDao Agent { get; set; }

        public string CountryId { get; set; }
        public CountryDao Country { get; set; }
    }
}