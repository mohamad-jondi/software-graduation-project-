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

        [ForeignKey("doctorID")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [ForeignKey("PatientID")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }

}
