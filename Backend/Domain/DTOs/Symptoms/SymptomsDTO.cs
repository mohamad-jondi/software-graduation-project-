namespace Domain.DTOs.Symptoms
{
    public class SymptomsDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Severity { get; set; }
        public DateTime WhenDidItStart { get; set; }
    }
}
