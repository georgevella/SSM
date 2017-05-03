using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SiteSpeedController.Master.Data.Contracts;

namespace SiteSpeedController.Master.Data.Models
{
    [Table("sites")]
    public class SiteDao : ICountryListContainer<SiteCountryAssociation>
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("domain")]
        public string Domain { get; set; }

        [Column("datastoreId")]
        public int? DataStoreId { get; set; }
        public DataStoreDao DataStore { get; set; }

        [Column("isEnabled")]
        public bool IsEnabled { get; set; }

        public List<PageDao> Pages { get; set; } = new List<PageDao>();

        public List<SiteCountryAssociation> Countries { get; set; } = new List<SiteCountryAssociation>();

        public List<SiteProfileAssociation> PerformanceProfiles { get; set; } = new List<SiteProfileAssociation>();
    }
}