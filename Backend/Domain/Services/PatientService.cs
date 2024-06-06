using Data.Models.Data.Models;
using Domain.DTOs.Doctor;
using Domain.DTOs.Patient;
using Domain.DTOs;
using Domain.IServices;
using Data.Interfaces;
using Data.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Data.enums;
using Domain.DTOs.Allergy;
using Domain.DTOs.Vaccination;
using Domain.DTOs.LifestyleFactors;

namespace Domain.Services
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PatientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DoctorForInputDTO>> BrowseDoctors(string? location, string? specialty, string? name)
        {
            var doctorsQuery = _unitOfWork.GetRepositories<Doctor>()
                .Get()
                .Include(d => d.Addresses)
                .Where(d =>
                    (string.IsNullOrEmpty(specialty) || d.Specialization == specialty) &&
                    (string.IsNullOrEmpty(name) || d.Name.Contains(name)) &&
                    (string.IsNullOrEmpty(location) || d.Addresses.Any(c =>
                        c.Country.Contains(location) ||
                        c.State.Contains(location) ||
                        c.City.Contains(location) ||
                        c.PostalCode.Contains(location) ||
                        c.StreetAddress.Contains(location)
                    ))
                );

            var doctors = await doctorsQuery.ToListAsync();
            return _mapper.Map<IEnumerable<DoctorForInputDTO>>(doctors);
        }

        public async Task<bool> AddEmergencyContact(string patientUsername, EmergencyContactInfoDTO emergencyContact)
        {
            var patient = await _unitOfWork.GetRepositories<Patient>()
                .Get()
                .Include(p => p.EmergencyContactInfo)
                .FirstOrDefaultAsync(p => p.Username == patientUsername);

            if (patient == null)
                return false;

            var newEmergencyContact = await _unitOfWork.GetRepositories<Person>()
                .Get()
                .FirstOrDefaultAsync(u => u.Username == emergencyContact.Username);

            if (newEmergencyContact == null)
                return false;

            var emergencyContactInfo = new EmergencyContactInfo
            {
                Relationship = emergencyContact.Relationship,
                Patient = patient,
                Person = newEmergencyContact
            };

            await _unitOfWork.GetRepositories<EmergencyContactInfo>().Add(emergencyContactInfo);
            
            return true;
        }

        public async Task<bool> RequestAppointment(string patientUsername, string doctorUsername, DateTime appointmentDate)
        {
            var patient = await _unitOfWork.GetRepositories<Patient>()
                .Get()
                .FirstOrDefaultAsync(p => p.Username == patientUsername);

            var doctor = await _unitOfWork.GetRepositories<Doctor>()
                .Get()
                .FirstOrDefaultAsync(d => d.Username == doctorUsername);

            if (patient == null || doctor == null)
                return false;

            var appointment = new Appointment
            {
                Date = appointmentDate,
                Doctor = doctor,
                Patient = patient,
                Status = AppointmentStatus.Pending 
            };

            await _unitOfWork.GetRepositories<Appointment>().Add(appointment);
            return true;
        }

        public async Task<DoctorForOutputDTO> RequestSecondOpinion(string patientUsername, string doctorUsername, string caseDescription)
        {
            // this still needs adjusting after giving the doctor full access 
            var patient = await _unitOfWork.GetRepositories<Patient>()
                .Get()
                .FirstOrDefaultAsync(p => p.Username == patientUsername);

            var doctor = await _unitOfWork.GetRepositories<Doctor>()
                .Get()
                .FirstOrDefaultAsync(d => d.Username == doctorUsername);

            if (patient == null || doctor == null)
                return null; 

            var appointment = new Appointment
            {
                Date = DateTime.UtcNow, 
                Doctor = doctor,
                Patient = patient,
                Status = AppointmentStatus.Pending, 
                Description = caseDescription 
            };
            await _unitOfWork.GetRepositories<Appointment>().Add(appointment);
            return _mapper.Map<DoctorForOutputDTO>(doctor);
        }

        public async Task<IEnumerable<AppointmentDTO>> ViewPastAppointments(string patientUsername)
        {
            var appointments = await _unitOfWork.GetRepositories<Appointment>()
                .Get()
                .Include(c=> c.Patient)
                .Include(d=> d.Doctor)
                .Where(a => a.Patient.Username == patientUsername && a.Status == AppointmentStatus.Ended)
                .ToListAsync();

            return _mapper.Map<IEnumerable<AppointmentDTO>>(appointments);
        }

        public async Task<IEnumerable<AppointmentDTO>> ViewUpcomingAppointments(string patientUsername)
        {
            var appointments = await _unitOfWork.GetRepositories<Appointment>()
                .Get()
                .Include(c=> c.Patient)
                .Where(a => a.Patient.Username == patientUsername && a.Date >= DateTime.Now && a.Status == AppointmentStatus.Accepted)
                .ToListAsync();

            return _mapper.Map<IEnumerable<AppointmentDTO>>(appointments);
        }

        public async Task<AllergyForOutputDTO> AddAllergy(string patientUsername, AllergyDTO allergy)
        {
            var patient = await _unitOfWork.GetRepositories<Patient>()
                .Get()
                .Include(p=>p.Allergies)
                .FirstOrDefaultAsync(p => p.Username == patientUsername);

            if (patient == null)
                return null;

            var newAllergy = _mapper.Map<Allergy>(allergy);
            newAllergy.Patient = patient;

            var x= await _unitOfWork.GetRepositories<Allergy>().Add(newAllergy);
            
            return _mapper.Map<AllergyForOutputDTO>(x);
        }

        public async Task<bool> DeleteAllergy(int allergyId)
        {
            var allergy = await _unitOfWork.GetRepositories<Allergy>().Get().Where(c=> c.AllergyID == allergyId).FirstOrDefaultAsync();
            if (allergy == null)
                return false;

            _unitOfWork.GetRepositories<Allergy>().Delete(allergy);
            return true;
        }

        public async Task<AllergyForOutputDTO> UpdateAllergy(int allergyId, AllergyDTO updatedAllergy)
        {
            var allergy = await _unitOfWork.GetRepositories<Allergy>().Get().Where(c=> c.AllergyID == allergyId).FirstOrDefaultAsync();
            if (allergy == null)
                return null;

            allergy.Allergey = !string.IsNullOrEmpty(updatedAllergy.Allergey) ? updatedAllergy.Allergey : allergy.Allergey;
            allergy.ReactionDescription = !string.IsNullOrEmpty(updatedAllergy.ReactionDescription) ? updatedAllergy.ReactionDescription : allergy.ReactionDescription;
            allergy.Severity = !string.IsNullOrEmpty(updatedAllergy.Severity) ? updatedAllergy.Severity : allergy.Severity;

            await _unitOfWork.GetRepositories<Allergy>().Update(allergy);
            return _mapper.Map<AllergyForOutputDTO>(allergy);
        }

        public async Task<IEnumerable<AllergyForOutputDTO>> BrowseAllargies(string PatientUsername)
        {
            var patient = await _unitOfWork.GetRepositories<Patient>()
                .Get()
                .Include(p => p.Allergies)
                .FirstOrDefaultAsync(p => p.Username == PatientUsername);

            if (patient == null)
                return Enumerable.Empty<AllergyForOutputDTO>(); 
            var allergyDTOs = patient.Allergies.Select(allergy => _mapper.Map<AllergyForOutputDTO>(allergy));

            return allergyDTOs;
        }


        public async Task<IEnumerable<VaccinationForOutputDTO>> BrowseVaccination(string PatientUsername)
        {
            var patient = await _unitOfWork.GetRepositories<Patient>()
                .Get()
                .Include(p => p.Vaccinations)
                .FirstOrDefaultAsync(p => p.Username == PatientUsername);

            if (patient == null)
                return Enumerable.Empty<VaccinationForOutputDTO>(); 
            var vaccinationDTOs = patient.Vaccinations.Select(vaccination => _mapper.Map<VaccinationForOutputDTO>(vaccination));

            return vaccinationDTOs;
        }


        public async Task<VaccinationForOutputDTO> AddVaccination(string PatientUsername, VaccinationDTO vaccination)
        {
            var patient = await _unitOfWork.GetRepositories<Patient>()
                .Get()
                .FirstOrDefaultAsync(p => p.Username == PatientUsername);

            if (patient == null)
                return null;


            var newAllergy = _mapper.Map<Vaccination>(vaccination);
            newAllergy.Patient = patient;

            var x = await _unitOfWork.GetRepositories<Vaccination>().Add(newAllergy);

            return _mapper.Map<VaccinationForOutputDTO>(x);
        }


        public async Task<bool> DeleteVaccination(int vaccinationId)
        {
            var vaccination = await _unitOfWork.GetRepositories<Vaccination>().Get().Where(c=> c.VaccinationID ==vaccinationId).FirstOrDefaultAsync();
            if (vaccination == null)
                return false; 

            await _unitOfWork.GetRepositories<Vaccination>().Delete(vaccination);
            return true;
        }
        public async Task<VaccinationForOutputDTO> UpdateVaccination(int vaccinationId, VaccinationForUpdatingDTO updatedVaccination)
        {
            var vaccination = await _unitOfWork.GetRepositories<Vaccination>().Get().Where(v=> v.VaccinationID == vaccinationId).FirstOrDefaultAsync();
            if (vaccination == null)
                return null;

            var vaccinationType = typeof(VaccinationForUpdatingDTO);
            var vaccinationProperties = vaccinationType.GetProperties();

            foreach (var property in vaccinationProperties)
            {
                var newValue = property.GetValue(updatedVaccination);
                if (newValue != null)
                {
                    var vaccinationProperty = vaccination.GetType().GetProperty(property.Name);
                    if (vaccinationProperty != null)
                    {
                        vaccinationProperty.SetValue(vaccination, newValue);
                    }
                }
            }

            var x= await _unitOfWork.GetRepositories<Vaccination>().Update(vaccination);

            return _mapper.Map<VaccinationForOutputDTO>(x);
        }

        public async Task<PatientFullDTO> ViewFullDetailsPatient(string patientUsername)
        {
            var patient = await _unitOfWork.GetRepositories<Patient>()
                .Get()
                .Include(p => p.EmergancyContact)
                .Include(p => p.Allergies)
                .Include(p => p.Vaccinations)
                .Include(p => p.cases)
                .FirstOrDefaultAsync(p => p.Username == patientUsername);


            if (patient == null)
                return null;

            var patientDTO = _mapper.Map<PatientFullDTO>(patient);
            return patientDTO;
        }

        public async Task<LifestyleFactorsDTO> AddLifeStyleFactors(string PatientUsername, LifestyleFactorsDTO LifeStyle)
        {
            var patient = await _unitOfWork.GetRepositories<Patient>()
             .Get().Include(c=> c.LifestyleFactors).Where(p => p.Username == PatientUsername).FirstOrDefaultAsync();

            if (patient == null)
                return null;
            if(patient.LifestyleFactors == null)
            {
                patient.LifestyleFactors = _mapper.Map<LifestyleFactors>(LifeStyle);
            }
            await _unitOfWork.GetRepositories<Patient>().Update(patient);
            return _mapper.Map<LifestyleFactorsDTO>(patient.LifestyleFactors);
        }

        public async Task<LifestyleFactorsDTO> UpdateLifeStyleFactors(string PatientUsername, LifestyleFactorsForUpdatingDTO LifeStyle)
        {
            var patient = await _unitOfWork.GetRepositories<Patient>()
              .Get().Include(c => c.LifestyleFactors).Where(p => p.Username == PatientUsername).FirstOrDefaultAsync();

            if (patient == null)
                return null;
            if (patient.LifestyleFactors == null)
            {
                patient.LifestyleFactors = _mapper.Map<LifestyleFactors>(LifeStyle);
            }
            else
            {
                patient.LifestyleFactors.DietaryPreferences = !string.IsNullOrEmpty(LifeStyle.DietaryPreferences) ? LifeStyle.DietaryPreferences : patient.LifestyleFactors.DietaryPreferences;
                patient.LifestyleFactors.IsAlcoholConsumer = LifeStyle.IsAlcoholConsumer.HasValue ?(bool) LifeStyle.IsAlcoholConsumer : patient.LifestyleFactors.IsAlcoholConsumer;
                patient.LifestyleFactors.IsSmoker = LifeStyle.IsSmoker.HasValue ? (bool)LifeStyle.IsSmoker : patient.LifestyleFactors.IsAlcoholConsumer;
                patient.LifestyleFactors.ExerciseHabits = !string.IsNullOrEmpty(LifeStyle.ExerciseHabits) ? LifeStyle.ExerciseHabits : patient.LifestyleFactors.ExerciseHabits;
            }
            await _unitOfWork.GetRepositories<Patient>().Update(patient);
            return _mapper.Map<LifestyleFactorsDTO>(patient.LifestyleFactors);
        }

        public async Task<LifestyleFactorsDTO> GetLifeStyleFactors(string PatientUsername)
        {
            var patient = await _unitOfWork.GetRepositories<Patient>()
              .Get().Include(c => c.LifestyleFactors).Where(p => p.Username == PatientUsername).FirstOrDefaultAsync();
            return _mapper.Map<LifestyleFactorsDTO>(patient.LifestyleFactors);

        }
    }


}
