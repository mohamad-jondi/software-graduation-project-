using Domain.DTOs;
using Domain.DTOs.Allergy;
using Domain.DTOs.Doctor;
using Domain.DTOs.Patient;
using Domain.DTOs.Vaccination;

namespace Domain.IServices
{
    public interface IPatientService
    {
        Task<IEnumerable<DoctorForInputDTO>> BrowseDoctors(string? location , string? specialty, string? name);
        Task<bool> AddEmergencyContact(string PatientUsername, EmergencyContactInfoDTO emergencyContact);
        Task<bool> RequestAppointment(string patientUsername, string doctorUsername, DateTime appointmentDate);
        Task<DoctorForOutputDTO> RequestSecondOpinion(string PatientUsername, string DoctorUsername, string caseDescription);
        Task<IEnumerable<AppointmentDTO>> ViewPastAppointments(string PatientUsername);
        Task<IEnumerable<AppointmentDTO>> ViewUpcomingAppointments(string PatientUsername);
        Task<IEnumerable<AllergyForOutputDTO>> BrowseAllargies (string PatientUsername);
        Task<AllergyForOutputDTO> AddAllergy(string PatientUsername, AllergyDTO allergy);
        Task<bool> DeleteAllergy(int allergyId);
        Task<AllergyForOutputDTO> UpdateAllergy(int allergyId, AllergyDTO updatedAllergy);
        Task<IEnumerable<VaccinationForOutputDTO>> BrowseVaccination(string PatientUsername);
        Task<VaccinationForOutputDTO> AddVaccination(string PatientUsername, VaccinationDTO vaccination);
        Task<bool> DeleteVaccination(int vaccinationId);
        Task<VaccinationForOutputDTO> UpdateVaccination(int vaccinationId, VaccinationForUpdatingDTO updatedVaccination);
        Task<PatientFullDTO > ViewFullDetailsPatient(string  PatientUsername);
        
    }

}
