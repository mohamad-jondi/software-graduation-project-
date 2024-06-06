using Data.enums;
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
        Task<bool> DeleteDoctorAvailability(int availabilityId, AvaliabilityDTO availabilityDTO);
        Task<AvaliabilityDTO> AddDoctorAvailability(int availabilityId, AvaliabilityDTO availabilityDTO);
        Task<AvaliabilityDTO> UpdateDoctorAvailability(int availabilityId, AvaliabilityDTO availabilityDTO);
        Task<IEnumerable<AvaliabilityDTO>> UpdateRangeDoctorAvailability(int availabilityId, IEnumerable<AvaliabilityDTO>availabilityDTO);

        Task<AppointmentReminderDTO> GetUpcomingAppointmentsReminders(string doctorsername);
        Task<bool> RequestAccessToPatientData(string doctorUsername, string patientUsername);
        Task<PatientFullDTO> ViewPatientHistory(string patientUsername);
        Task<IEnumerable<DocumentDTO>> ViewPatientDocuments(string patientUsername);
        Task<CredentialDTO> UpdateDoctorCredential(string doctorUsername, CredentialDTO DoctorCredential);
        Task<bool> DeleteDoctorCredential(string doctorUsername, CredentialDTO DoctorCredential);
        Task<CredentialDTO> AddDoctorCredential(string doctorUsername, CredentialDTO DoctorCredential);
        Task<bool> UpdateDoctorTypeOfWork(string doctorUsername, DoctorWorkType specializationDTO);

        //  Task<bool> AddPatientNote(string doctorUsername, string patientUsername, PatientNoteDTO patientNoteDTO);
        //  Task<IEnumerable<PatientNoteDTO>> GetPatientNotes(string patientUsername);
        //  Task<bool> HandleSecondOpinionRequest(string doctorUsername, SecondOpinionRequestDTO requestDTO);
        //Task<HealthReportDTO> GeneratePatientHealthReport(string patientUsername, DateTime startDate, DateTime endDate);
        // Task<bool> CheckDrugInteractions(string patientUsername, DrugDTO newDrugDTO);
        //Task<bool> AddDrugToPatientHistory(string doctorUsername, string patientUsername, DrugDTO drugDTO);
    }
}
