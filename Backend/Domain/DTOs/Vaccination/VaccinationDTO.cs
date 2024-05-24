using Data.enums;

namespace Domain.DTOs.Vaccination
{
    public class VaccinationDTO
    {
        public string Name { get; set; }
        public DateTime AdministeredDate { get; set; }
        public string Description { get; set; }
        public VaccineStatus VaccineStatus { get; set; }
        public int ShotsLeft { get; set; }
    }
}
