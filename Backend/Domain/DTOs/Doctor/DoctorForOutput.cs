using Domain.DTOs.Appointment;
using Domain.DTOs.Cases;
using Domain.DTOs.Chats;

namespace Domain.DTOs.Doctor
{
    public class DoctorForOutputDTO
    {

        public string Specialization { get; set; }
        public string DoctorWorkType { get; set; }
        public ICollection<CredentialDTO> credential { get; set; }
        public ICollection<ChatMessageDTO> Chats { get; set; }
        public ICollection<AppointmentDTO> appointments { get; set; }
        public ICollection<AvaliabilityDTO> Avalible { get; set; }
        public ICollection<CaseDTO> Cases{ get; set; }

    }
}
