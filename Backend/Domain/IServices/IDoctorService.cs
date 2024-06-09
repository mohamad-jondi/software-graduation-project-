using Data.enums;
using Domain.DTOs;
using Domain.DTOs.Cases;
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
        Task<IEnumerable<AvaliabilityDTO>> UpdateRangeDoctorAvailability(int availabilityId, IEnumerable<AvaliabilityDTO> availabilityDTO);

        Task<AppointmentReminderDTO> GetUpcomingAppointmentsReminders(string doctorsername);
        Task<bool> RequestAccessToPatientData(string doctorUsername, string patientUsername);
        Task<PatientFullDTO> ViewPatientHistory(string patientUsername);
        Task<IEnumerable<DocumentDTO>> ViewPatientDocuments(string patientUsername);
        Task<CredentialDTO> UpdateDoctorCredential(string doctorUsername, CredentialDTO DoctorCredential);
        Task<bool> DeleteDoctorCredential(string doctorUsername, CredentialDTO DoctorCredential);
        Task<CredentialDTO> AddDoctorCredential(string doctorUsername, CredentialDTO DoctorCredential);
        Task<bool> UpdateDoctorTypeOfWork(string doctorUsername, DoctorWorkType specializationDTO);

        // Added methods for managing cases and tests
        Task<IEnumerable<CaseDTO>> ViewCases(string doctorUsername);
        Task<bool> ManageCase(int caseId, CaseDTO caseDTO);
        Task<IEnumerable<TestDTO>> GetTests(string doctorUsername);
        Task<TestDTO> AddTest(string doctorUsername, TestDTO testDTO);
        Task<bool> UpdateTestStatus(int testId, TestStatus status);
        Task<bool> DeleteTest(int testId);

        // Added methods for managing surgeries
        Task<IEnumerable<SurgeryDTO>> ViewSurgeries(string doctorUsername);
        Task<bool> ScheduleSurgery(string doctorUsername, SurgeryDTO surgeryDTO);
        Task<bool> UpdateSurgery(int surgeryId, SurgeryDTO surgeryDTO);
        Task<bool> CancelSurgery(int surgeryId);

        // Added methods for managing documents
        Task<IEnumerable<DocumentDTO>> ViewDocuments(string doctorUsername);
        Task<DocumentDTO> UploadDocument(string doctorUsername, DocumentDTO documentDTO);
        Task<bool> DeleteDocument(int documentId);
    }
}
