using Data.enums;
using System;

namespace Domain.DTOs
{
    public class MedicalSecondOpinionDTO
    {
        public int SecondOpinionId { get; set; }
        public int CaseId { get; set; }
        public int ReviewingDoctorId { get; set; }
        public DateTime? ReviewDate { get; set; }
        public MedicalSecondOpinionStatus Status { get; set; }
        public string SecondOpinionDiagnosis { get; set; }
        public string Comments { get; set; }
    }
}
