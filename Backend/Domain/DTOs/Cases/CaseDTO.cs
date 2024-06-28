using Data.Models;
using Domain.DTOs.Appointment;
using Domain.DTOs.Symptoms;

namespace Domain.DTOs.Cases
{
    public class CaseDTO
    {
        public int CaseId { get; set; }
        public string PatientUsername { get; set; }
        public string DoctorUserName { get; set; }
        public string NurseUserName { get; set; }
        public string CaseDescription { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Diagnosis { get; set; }
        public string Notes { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime? NextAppointmentDate { get; set; }
        public ICollection<SymptomsDTO > Symptoms { get; set; }
        public ICollection<RelatedDocumentDTO> RelatedDocuments { get; set; }
        public ICollection<MedicalSecondOpinionDTO> SecondOpinionRequests { get; set; }
        public ICollection<TestDTO> Tests { get; set; }
        public ICollection<AppointmentDTO> Appointments { get; set; }
        public ICollection<DrugDTO> Drugs{ get; set; }
    }
}
