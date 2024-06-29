using Domain.DTOs.Appointment;
using Domain.DTOs.Child;
using Domain.DTOs.Vaccination;

public interface IMotherService
{
    Task<ChildDTO> AddChildAsync(string motherUsername, ChildForCreationDTO childDTO);
    Task<ChildDTO> UpdateChildInfo(int ChildID, ChildDTO childDTO);
    Task<VaccinationDTO> AddChildVacination(int ChildID, VaccinationDTO vacine);
    Task<bool> DeleteChildVacination (int vaccinationID);
    Task<bool> UpdateChildVaccinationAsync(VaccinationForUpdatingDTO vaccinationDTO);
    Task<AppointmentDTO> BookAppointment(int ChildID, string DoctorUserName, AppointmentDTO appointment);
    Task<bool> CancelChildAppointmentAsync(int childId, AppointmentCanceltionDTO appointmentDTO);
    Task<IEnumerable<ChildDTO>> GetChildrenAsync(string motherUsername);
    Task<ChildDTO> GetChildByID(int  ChildId);
    
}
