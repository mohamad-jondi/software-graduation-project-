namespace Domain.DTOs.Cases
{
    public class CaseForCreationDTO
    {
        public string PatientUsername { get; set; }
        public string? DoctorUserName { get; set; }
        public string? NurseUserName { get; set; }
        public string CaseDescription { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Notes { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
