using Domain.DTOs;
using Domain.DTOs.Vaccination;

public interface IMotherService
{
    Task<ChildDTO> AddChildAsync(string motherUsername, ChildDTO childDTO);
    Task<bool> ManageChildVaccinationAsync(int childId, VaccinationDTO vaccinationDTO);
    Task<bool> ManageChildAppointmentAsync(int childId, AppointmentDTO appointmentDTO);
    Task<IEnumerable<ChildDTO>> GetChildrenAsync(string motherUsername);
    Task<bool> SnoozeDoctorAppointmentsAsync(string doctorUsername, int minutes);
    Task<bool> MoveAppointmentAsync(int appointmentId, int minutes);
}
