using Data.enums;

namespace Domain.DTOs.Appointment
{
    public class AppointmentMangmentDTO
    {
        public int appointmentId {  get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
    }
}
