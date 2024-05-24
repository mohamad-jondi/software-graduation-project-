namespace Domain.DTOs.Vaccination
{
     public class VaccinationForOutputDTO
    {
        public int VaccinationID { get; set; }
        public string Name { get; set; }
        public DateTime AdministeredDate { get; set; }
        public string Description { get; set; }
        public string VaccineStatus { get; set; }
        public int ShotsLeft { get; set; }
    }
}
