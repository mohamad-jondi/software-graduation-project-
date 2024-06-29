using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class DoctorRating : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoctorRatingID { get; set; }
        [Range(0,5)]
        public double Rating { get; set; }
        public string? comment { get; set; }

        [ForeignKey("DoctorID")]
        public int DoctorID { get; set; }
        public Doctor? Doctor { get; set; }

        [ForeignKey("UserID")]
        public int UserID { get; set; } 
        public User? User{ get; set; }

    }
}
