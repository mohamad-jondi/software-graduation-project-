using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Data.Models
{
    public class Case
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CaseId { get; set; }

        [ForeignKey("PatientID")]
        public int PatientID { get; set; }
        [ForeignKey("DoctorID")]
        public int DoctorId { get; set; }
        [ForeignKey("NurseID")]
        public int NurseID { get; set; }
        public string CaseDescription { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Diagnosis { get; set; }
        public string TreatmentPlan { get; set; }
        public string Notes { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public Nurse Nurse { get; set; }
        public ICollection<Document> RelatedDocuments { get; set; }

        public ICollection<Operation> RelatedOperations { get; set; } 
        public ICollection<MedicalSecondOpinion> SecondOpinionRequests { get; set; }
    }

}
