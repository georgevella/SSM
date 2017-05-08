using System.ComponentModel.DataAnnotations.Schema;

namespace SiteSpeedController.Master.Data.Models
{
    [Table("siteProfiles")]
    public class SiteProfileAssociation
    {
        [Column("siteId")]
        public int SiteId { get; set; }
        public SiteDao Site { get; set; }

        [Column("profileId")]
        public string ProfileId { get; set; }
        public PerformanceProfileDao Profile { get; set; }
    }
}