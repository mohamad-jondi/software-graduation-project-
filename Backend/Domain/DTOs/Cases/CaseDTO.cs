namespace Domain.DTOs.Cases
{
    public class CaseDTO
    {
        public int CaseId { get; set; }
        public int PatientID { get; set; }
        public int DoctorId { get; set; }
        public int NurseID { get; set; }
        public string CaseDescription { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Diagnosis { get; set; }
        public string Notes { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
        public ICollection<DocumentDTO> RelatedDocuments { get; set; }
        public ICollection<SurgeryDTO> RelatedSurgeries { get; set; }
        public ICollection<MedicalSecondOpinionDTO> SecondOpinionRequests { get; set; }
        public ICollection<AppointmentDTO> Appointments { get; set; }
        public TreatmentPlanDTO TreatmentPlan { get; set; }
    }
}
