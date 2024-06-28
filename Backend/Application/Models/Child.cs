using Data.enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Child :BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double? LatestRecordedWeight { get; set; }
        public double? LatestRecordedHeight { get; set; }
        public Gender Gender { get; set; }

        [ForeignKey("MotherId")]
        public int MotherId { get; set; }

        public Mother Parent { get; set; }
        public List<Case> Cases { get; set; }
        public List<Appointment> Appointments { get; set; }

        public List<Vaccination> Vaccination { get; set; }
    }
}
