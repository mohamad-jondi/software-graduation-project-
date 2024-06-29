using Data.Models;
using Domain.DTOs.Appointment;

namespace Domain.DTOs.Doctor
{
    public class DoctorForBrowsingDTO
    {

        public string Username { get; set; }
        public string Specialization { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public IEnumerable<AppointmentDTO> Appointment { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<AvaliabilityDTO> Avalible { get; set; }

        public string Url { get; set; }
    }
}
