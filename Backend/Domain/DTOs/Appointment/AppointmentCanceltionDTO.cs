namespace Domain.DTOs.Appointment
{
    public class AppointmentCanceltionDTO
    {
        public int appointmentId { get; set; }
        public string CanceledBy { get; set; }
        public string? CanceledReson { get; set; }
    }
}
