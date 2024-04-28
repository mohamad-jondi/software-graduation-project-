using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Chat : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChatID { get; set; }
        [ForeignKey("PatientID")]
        public int PatientID { get; set; }
        public Patient? Patient { get; set; }
        [ForeignKey("DoctorID")]
        public int DoctorID { get; set; }
        public Doctor? Doctor { get; set; } 
        public ICollection<ChatMessage> Messages { get; set; } 
    }

}