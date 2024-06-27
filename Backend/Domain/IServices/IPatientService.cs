using Domain.DTOs.Allergy;
using Domain.DTOs.Appointment;
using Domain.DTOs.Doctor;
using Domain.DTOs.LifestyleFactors;
using Domain.DTOs.Patient;
using Domain.DTOs.Vaccination;

namespace Domain.IServices
{
    public interface IPatientService
    {
        Task<IEnumerable<DoctorForBrowsingDTO>> BrowseDoctors(string? location , string? specialty, string? name);
        Task<bool> AddEmergencyContact(string PatientUsername, EmergencyContactInfoDTO emergencyContact);
        Task<bool> RequestAppointment(string patientUsername, string doctorUsername, DateTime appointmentDate);
        Task<DoctorForOutputDTO> RequestSecondOpinion(string PatientUsername, string DoctorUsername, string caseDescription);
        Task<IEnumerable<AppointmentDTO>> ViewPastAppointments(string PatientUsername);
        Task<IEnumerable<AppointmentDTO>> ViewUpcomingAppointments(string PatientUsername);
        Task<IEnumerable<AllergyForOutputDTO>> BrowseAllargies (string PatientUsername);
        Task<AllergyForOutputDTO> AddAllergy(string PatientUsername, AllergyDTO allergy);
        Task<bool> DeleteAllergy(int allergyId);
        Task<AllergyForOutputDTO> UpdateAllergy(int allergyId, AllergyDTO updatedAllergy);
        Task<PatientFullDTO > ViewFullDetailsPatient(string  PatientUsername);

        Task<LifestyleFactorsDTO> GetLifeStyleFactors(string PatientUsername);
        Task<LifestyleFactorsDTO> AddLifeStyleFactors(string PatientUsername, LifestyleFactorsDTO LifeStyle);
        Task<LifestyleFactorsDTO> UpdateLifeStyleFactors(string PatientUsername, LifestyleFactorsForUpdatingDTO LifeStyle);

    }

}
