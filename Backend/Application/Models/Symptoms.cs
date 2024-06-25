using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Symptoms : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SymptomID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Severity { get; set; } 
        public TimeSpan Duration { get; set; } 
        public DateTime WhenDidItStart { get; set; } 
    }
}
