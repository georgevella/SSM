using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteSpeedController.Master.Data.Models
{
    [Table("pages")]
    public class PageDao
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("path")]
        public string Path { get; set; }

        [Column("alias")]
        public string Alias { get; set; }

        [Column("runs")]
        public int RunsTriggered { get; set; }

        public List<PageCountryAssociation> Countries { get; set; } = new List<PageCountryAssociation>();
    }
}