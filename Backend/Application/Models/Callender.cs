using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Callender : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CallenderID { get; set; } 
        public DateTime Date { get; set; }
        public string Type { get; set; } 
        public string Description { get; set; }

        [ForeignKey("PersonID")]
        public int PersonID { get; set; }
        public Person? Patient { get; set; }
    }
}
