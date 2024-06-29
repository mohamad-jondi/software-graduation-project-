namespace Domain.DTOs
{
    public class VaccinationForOutputingDTO
    {
        public string Name { get; set; }
        public DateTime AdministeredDate { get; set; }
        public string Description { get; set; }
        public string VaccineStatus { get; set; }
        public int ShotsLeft { get; set; }
    }
}
