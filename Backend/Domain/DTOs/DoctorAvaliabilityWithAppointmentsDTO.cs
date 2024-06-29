using Domain.DTOs.Appointment;

namespace Domain.DTOs
{
    public class DoctorAvaliabilityWithAppointmentsDTO
    {
       public IEnumerable <AvaliabilityDTO> Avaliabilities { get ; set; } 
        public IEnumerable<AppointmentDTO> Appointments { get; set; }
    }
}
