using SiteSpeedManager.Master.Data.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteSpeedManager.Master.Data.Models
{
    [Table("sites")]
    public class SiteDao : ICountryListContainer<SiteCountryAssociation>
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("domain")]
        public string Domain { get; set; }

        [Column("timingStoreId")]
        public string TimingStoreId { get; set; }
        public DataStoreDao TimingStore { get; set; }

        [Column("resultStoreId")]
        public string ResultStoreId { get; set; }
        public DataStoreDao ResultStore { get; set; }

        [Column("isEnabled")]
        public bool IsEnabled { get; set; }

        public List<PageDao> Pages { get; set; } = new List<PageDao>();

        public List<SiteCountryAssociation> Countries { get; set; } = new List<SiteCountryAssociation>();

        public List<SiteProfileAssociation> PerformanceProfiles { get; set; } = new List<SiteProfileAssociation>();
    }
}