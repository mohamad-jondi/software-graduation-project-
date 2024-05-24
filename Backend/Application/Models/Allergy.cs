using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Allergy :BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AllergyID { get; set; }

        [ForeignKey("PatientID")]
        public int PatientID { get; set; }
        public Patient? Patient { get; set; } 
        public string Allergey { get; set; }
        public string ReactionDescription { get; set; }
        public string Severity { get; set; }
    }
}