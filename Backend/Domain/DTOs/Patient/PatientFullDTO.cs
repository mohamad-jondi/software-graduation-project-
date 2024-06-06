using Domain.DTOs.Allergy;
using Domain.DTOs.Cases;
using Domain.DTOs.Doctor;
using Domain.DTOs.LifestyleFactors;
using Domain.DTOs.Vaccination;

namespace Domain.DTOs.Patient
{
    public class PatientFullDTO
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double Age { get; set; }
        public double? LatestRecordedWeight { get; set; }
        public double? LatestRecordedHeight { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Occupation { get; set; }

        public LifestyleFactorsDTO LifestyleFactorsDTO { get; set; }
        public EmergencyContactInfoDTO EmergancyContact { get; set; }

        public ICollection<OperationDTO> operations { get; set; }
        public ICollection<AllergyDTO> Allergies { get; set; }
        public ICollection<VaccinationDTO> Vaccinations { get; set; }
        public ICollection<CaseDTO> Cases { get; set; }

        public ICollection<AppointmentDTO> appointments { get; set; }
    }
}
