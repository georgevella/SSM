using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteSpeedController.Master.Data.Models
{
    [Table("performanceProfiles")]
    public class PerformanceProfileDao
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        public ConnectionSpeedType Speed { get; set; }

        public int CustomDownstream { get; set; }

        public int CustomUpstream { get; set; }

        public int ViewportWidth { get; set; }

        public int ViewportHeight { get; set; }

        public string UserAgent { get; set; }

        public bool EnableSpeedIndex { get; set; }

        public bool EnableVideo { get; set; }
    }
}