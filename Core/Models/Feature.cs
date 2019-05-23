using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AspAng.Core.Models;

namespace AspAng.Core.Models
{
    [Table("Features")]
    public class Feature
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        
    }
}