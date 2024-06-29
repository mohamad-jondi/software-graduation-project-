using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Avaliability : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AvalibailityID { get; set; }
        public DayOfWeek DayOfWeek { get; set; } 
        public TimeSpan StartHour { get; set; }
        public TimeSpan EndHour { get; set; }

        [ForeignKey("DoctorID")]
        public int DoctorID { get; set; }
        public Doctor? Doctor { get; set; }
    }
}
