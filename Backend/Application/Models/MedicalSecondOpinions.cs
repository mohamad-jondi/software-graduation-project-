using Data.enums;
using System.ComponentModel.DataAnnotations.Schema;
namespace Data.Models
{
    public class MedicalSecondOpinion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SecondOpinionId { get; set; }
        [ForeignKey("CaseID")]
        public int CaseId { get; set; }
        [ForeignKey("DoctorID")]
        public int ReviewingDoctorId { get; set; }
        public DateTime? ReviewDate { get; set; }
        public MedicalSecondOpinionStatus Status { get; set; } 
        public string SecondOpinionDiagnosis { get; set; }
        public string Comments { get; set; }
        public Case Case { get; set; }
        public Doctor ReviewingDoctor { get; set; }
    }

}
