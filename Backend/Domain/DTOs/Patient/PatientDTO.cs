﻿using Domain.DTOs.Allergy;
using Domain.DTOs.Cases;
using Domain.DTOs.Doctor;
using Domain.DTOs.LifestyleFactors;
using Domain.DTOs.Vaccination;

namespace Domain.DTOs.Patient
{
    public class PatientDTO
    {
        public string Occupation { get; set; }

        public LifestyleFactorsDTO LifestyleFactors { get; set; }
        public EmergencyContactInfoDTO EmergancyContact { get; set; }

        public ICollection<OperationDTO> operations { get; set; }
        public ICollection<AllergyDTO> Allergies { get; set; }
        public ICollection<VaccinationDTO> Vaccinations { get; set; }

        public ICollection<CaseDTO> Cases{ get; set; }
    }
}
