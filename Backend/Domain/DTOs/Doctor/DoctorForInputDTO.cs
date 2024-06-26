﻿using Data.enums;
using Domain.DTOs.Appointment;
using Domain.DTOs.Cases;
using Domain.DTOs.Chats;

namespace Domain.DTOs.Doctor
{
    public class DoctorForInputDTO
    {
        public string Username { get; set; }
        public string Specialization { get; set; }
        public DoctorWorkType DoctorWorkType { get; set; }
        public ICollection<CredentialDTO> credential { get; set; }
        public ICollection<ChatMessageDTO> Chats { get; set; }
        public ICollection<AppointmentDTO> AppointmentDTO { get; set; }
        public ICollection<AvaliabilityDTO> Avalible { get; set; }
        public ICollection<CaseDTO> Cases { get; set; }
    }
}
