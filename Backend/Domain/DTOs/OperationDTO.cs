using Domain.DTOs.Doctor;
namespace Domain.DTOs
{
    public class OperationDTO
    {
        public string OperationName { get; set; }
        public DateTime SurgeryDate { get; set; }
        public string DoctorName { get; set; }
        public string Description { get; set; }
    }
}
