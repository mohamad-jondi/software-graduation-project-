using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Credential :BaseEntity 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CredentialID { get; set; }
        [Required]
        public string CredentialType { get; set; }

        [Required]
        public string CredentialValue { get; set; }

        [ForeignKey("DoctorID")]
        public int DoctorID { get; set; }
        public Doctor Doctor { get; set; }
    }
}
