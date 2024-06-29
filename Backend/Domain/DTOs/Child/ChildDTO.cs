using Data.enums;
using Data.Models;
using Domain.DTOs.Appointment;
using Domain.DTOs.Cases;
using Domain.DTOs.Vaccination;

namespace Domain.DTOs.Child
{
    public class ChildDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double? LatestRecordedWeight { get; set; }
        public double? LatestRecordedHeight { get; set; }
        public Gender Gender { get; set; }
        public List<CaseDTO> cases { get; set; }
        public List<AppointmentDTO> Appointments { get; set; }
        public List<VaccinationDTO> Vaccination { get; set; }
    }
}
