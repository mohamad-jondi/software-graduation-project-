using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Documents
    {
        public string Type { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long DocumentId { get; set; }
        [ForeignKey("UserID")]
        public long UserID { get; set; }
        public User? User{ get; set; }
        public string FileName { get; set; }
        [Required]
        public byte[]? FileData { get; set; }
    }
}
