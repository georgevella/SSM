using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteSpeedController.Master.Data.Models
{
    [Table("countries")]
    public class CountryDao
    {
        [Column("id")]
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}