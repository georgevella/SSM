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

        [Column("speed")]
        public ConnectionSpeedType Speed { get; set; }

        [Column("downstream")]
        public int CustomDownstream { get; set; }

        [Column("upstream")]
        public int CustomUpstream { get; set; }

        [Column("viewportWidth")]
        public int ViewportWidth { get; set; }

        [Column("viewportHeight")]
        public int ViewportHeight { get; set; }

        [Column("useragent")]
        public string UserAgent { get; set; }

        [Column("speedIndexEnabled")]
        public bool SpeedIndexEnabled { get; set; }

        [Column("videoEnabled")]
        public bool VideoEnabled { get; set; }
    }
}