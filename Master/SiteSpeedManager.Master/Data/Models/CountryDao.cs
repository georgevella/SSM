using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteSpeedManager.Master.Data.Models
{
    [Table("countries")]
    public class CountryDao
    {
        [Column("id")]
        [Key]
        public string Id { get; set; }

        [Column("name")]

        public string Name { get; set; }

        [Column("isDisabled")]
        public bool IsEnabled { get; set; }
    }
}