namespace Domain.DTOs.Cases
{
    public class CaseForUpdating
    {
        public string? CaseDescription { get; set; }
        public string? Title { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Diagnosis { get; set; }
    }
}
