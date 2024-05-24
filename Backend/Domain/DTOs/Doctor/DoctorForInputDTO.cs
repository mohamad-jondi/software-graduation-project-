using Data.enums;
using Domain.DTOs.Patient;

namespace Domain.DTOs.Doctor
{
    public class DoctorForInputDTO
    {
        public string Username { get; set; }
        public string Specialization { get; set; }
        public DoctorWorkType DoctorWorkType { get; set; }
        public ICollection<CredentialDTO> credential { get; set; }
        public ICollection<ChatDTO> Chats { get; set; }
        public ICollection<CallenderDTO> Callender { get; set; }
        public ICollection<AvaliabilityDTO> Avalible { get; set; }
        public ICollection<PatientDTO> Patients { get; set; }
    }
}
