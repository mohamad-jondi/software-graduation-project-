using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Documents : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public string Url { get; set; }

        [ForeignKey("CaseID")]
        public int CaseID { get; set; }
        public Case Case { get; set; }
    }
}
