using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    namespace Data.Models
    {
        public class EmergencyContactInfo : BaseEntity
        {
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int EmergencyContactInfoID { get; set; } 
            public string Relationship { get; set; }

            [ForeignKey("PatientID")]
            public int PatientId { get; set; }
            public Patient Patient { get; set; }

            [ForeignKey("PersonID")]
            public int EmergancyContactID { get; set; }
            public Person Person { get; set; }
        }
    }

}
