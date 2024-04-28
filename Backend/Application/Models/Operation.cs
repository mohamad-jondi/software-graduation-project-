using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Operation :BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OperationID { get; set; }
        
        [ForeignKey("PatientID")]
        public int PatientID { get; set; } 
        public Patient? Patient { get; set; } 
        public string OperationName { get; set; }
        public DateTime SurgeryDate { get; set; }
        [ForeignKey("DoctorID")]
        public int SurgeonID { get; set; }
        public Doctor? Surgeon { get; set; }
        public string Description { get; set; }
        
    }
}
