using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Case : BaseEntity
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
        public DateTime LastUpdated { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public Nurse Nurse { get; set; }
        public ICollection<Symptoms> symptoms { get; set; }
        public ICollection<Test> Tests { get; set; }  
        public ICollection<Documents> RelatedDocuments { get; set; }
        public ICollection<Surgery> RelatedOperations { get; set; } 
        public ICollection<MedicalSecondOpinion> SecondOpinionRequests { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public TreatmentPlan TreatmentPlan { get; set; } 
    }

}
