using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Avaliability : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AvalibailityID { get; set; }
        public DateTime AvaliabilityTimeStart { get; set; }
        public DateTime AvaliabilityTimeEnd { get; set; }
        public DateTime AvaliabilityDate { get; set; }

        [ForeignKey("DoctorID")]
        public int DoctorID { get; set; }
        public Doctor? Doctor { get; set; }
    }
}