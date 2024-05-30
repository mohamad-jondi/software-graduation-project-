using Domain.DTOs;
using Domain.DTOs.Patient;

namespace Domain.IServices
{
    public interface IDoctorService
    {
        Task<IEnumerable<AppointmentDTO>> GetAccpetedAppointments(string doctorUsername);
        Task<IEnumerable<AppointmentDTO>> GetPendingAppointments(string doctorUsername);
        Task<bool> ManageAppointment(int appointmentId, AppointmentDTO appointmentDTO);
        Task<IEnumerable<AvaliabilityDTO>> GetDoctorAvailability(string doctorUsername);
        Task<bool> UpdateDoctorAvailability(int availabilityId, AvaliabilityDTO availabilityDTO);
        Task<bool> UpdateRangeDoctorAvailability(int availabilityId, IEnumerable<AvaliabilityDTO>availabilityDTO);

        //Task<bool> AddDrugToPatientHistory(string doctorUsername, string patientUsername, DrugDTO drugDTO);
        Task<AppointmentReminderDTO> GetUpcomingAppointmentsReminders(string doctorUsername);
        Task<bool> RequestAccessToPatientData(string doctorUsername, string patientUsername);
        Task<bool> CheckDrugInteractions(string patientUsername, DrugDTO newDrugDTO);
        Task<PatientFullDTO> ViewPatientHistory(string patientUsername);
        Task<bool> UpdateDoctorProfile(string doctorUsername, DoctorProfileDTO doctorProfileDTO);
        Task<bool> AddPatientNote(string doctorUsername, string patientUsername, PatientNoteDTO patientNoteDTO);
        Task<IEnumerable<PatientNoteDTO>> GetPatientNotes(string patientUsername);
        Task<bool> UpdateSpecializationAndCredentials(string doctorUsername, DoctorSpecializationDTO specializationDTO);
        Task<bool> RespondToAccessRequest(int requestId, AccessRequestResponseDTO responseDTO);
        Task<bool> HandleSecondOpinionRequest(string doctorUsername, SecondOpinionRequestDTO requestDTO);
        Task<HealthReportDTO> GeneratePatientHealthReport(string patientUsername, DateTime startDate, DateTime endDate);
    }
}
