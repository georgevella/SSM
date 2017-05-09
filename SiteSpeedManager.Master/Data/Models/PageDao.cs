using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SiteSpeedManager.Master.Data.Contracts;

namespace SiteSpeedManager.Master.Data.Models
{
    [Table("pages")]
    public class PageDao : ICountryListContainer<PageCountryAssociation>
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("path")]
        public string Path { get; set; }

        [Column("alias")]
        public string Alias { get; set; }

        [Column("isEnabled")]
        public bool IsEnabled { get; set; }

        [Column("overridesCountryList")]
        public bool OverridesSiteCountryList { get; set; }

        [Column("siteId")]
        public int SiteId { get; set; }
        public SiteDao Site { get; set; }

        public List<PageCountryAssociation> Countries { get; set; } = new List<PageCountryAssociation>();
    }
}