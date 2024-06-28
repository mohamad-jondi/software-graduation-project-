using Data.enums;

namespace Domain.DTOs.Appointment
{
    public class AppointmentDTO
    {
        public DateTime Date { get; set; }
        public string? DoctorName { get; set; }
        public string? PatientName { get; set; }
        public string Status { get; set; }
        public string? Description { get; set; }

        public string? Notes { get; set; }
    }

}
