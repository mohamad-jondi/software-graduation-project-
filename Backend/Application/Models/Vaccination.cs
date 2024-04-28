using Data.enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Vaccination : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VaccinationID { get; set; }
        [ForeignKey("PatientID")]         
        public int PatientID { get; set; } 
        public Patient? Patient { get; set; }
        public string Name { get; set; }
        public DateTime AdministeredDate { get; set; }
        public string Description { get; set; }
        public VaccineStatus VaccineStatus { get; set; }
        public int ShotsLeft { get; set;}
    }
}