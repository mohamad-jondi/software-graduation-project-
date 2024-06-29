using Domain.DTOs.Appointment;

namespace Domain.IServices
{
    public interface IAppointmentsService
    {
        Task<IEnumerable<AppointmentDTO>> GetAppointments(string UserName);
        Task<AppointmentDTO> GetAppointmentByID(string UserName);
        Task<AppointmentDTO> ManageAppointment(string UserName, AppointmentMangmentDTO appointment);
        Task<IEnumerable<AppointmentDTO>> SnozeDoctorAppointments(string DoctorUserName, string minites);
        Task<AppointmentDTO> CancelAppointment(int AppointmentID, AppointmentCanceltionDTO cancelApontmentDTO);
        Task<AppointmentDTO> MoveAppointment(string username, int AppointmentID);
    }
}
