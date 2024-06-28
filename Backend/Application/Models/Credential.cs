using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Credential :BaseEntity 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CredentialID { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public string Url { get; set; }

        [ForeignKey("DoctorID")]
        public int DoctorID { get; set; }
        public Doctor Doctor { get; set; }
    }
}
