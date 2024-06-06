namespace Domain.DTOs.Cases
{
    public class CaseDTO
    {
        public int CaseId { get; set; }
        public string CaseDescription { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Diagnosis { get; set; }
        public string TreatmentPlan { get; set; }
        public string Notes { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
    }
}
