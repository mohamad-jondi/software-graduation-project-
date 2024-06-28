using Data.Models;

namespace Domain.DTOs.Doctor
{
    public class DoctorWithCredinttialsDTO
    {
        public string Username { get; set; }
        public string Specialization { get; set; }
        public ICollection<CredentialForShowDTO> credential { get; set; }
    }
}
