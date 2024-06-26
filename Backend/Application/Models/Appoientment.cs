using Data.enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{

    public class Appointment : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentId { get; set; }

        public DateTime Date { get; set; }

        public AppointmentStatus Status { get; set; }

        public string Description { get; set; }

        public string DoctorNotes { get; set; }

        [ForeignKey("doctorID")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [ForeignKey("PatientID")]
        public int? PatientId { get; set; }
        public Patient? Patient { get; set; }

        [ForeignKey("ChildId")]
        public int? ChildID { get; set; }
        public Child? childPAtient { get; set; }

        public string? CanceledBy { get; set; }
        public string? CanceledReson { get; set; }
    }

}
