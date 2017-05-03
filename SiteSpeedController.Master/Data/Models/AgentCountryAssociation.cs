﻿using System.ComponentModel.DataAnnotations.Schema;

namespace SiteSpeedController.Master.Data.Models
{
    [Table("agentCountries")]
    public class AgentCountryAssociation
    {
        [Column("agentId")]
        public int AgentId { get; set; }
        public AgentDao Agent { get; set; }

        [Column("countryId")]
        public string CountryId { get; set; }
        public CountryDao Country { get; set; }
    }
}