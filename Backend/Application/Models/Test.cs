using Data.enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Test : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TestID { get; set; }

        [ForeignKey("PatientID")]
        public int PatientID { get; set; }
        public Patient Patient { get; set; }

        [Required]
        public string TestName { get; set; }

        public string Description { get; set; }
        public TestStatus Status { get; set; }
        public string Results { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime? DateCompleted { get; set; }
    }
}
