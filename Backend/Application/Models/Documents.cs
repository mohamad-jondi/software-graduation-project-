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
        public int UserID { get; set; }
        public User? User{ get; set; }
        public string FileName { get; set; }
        [Required]
        [MaxLength(25 * 1024 * 1024)]
        public byte[] FileData { get; set; }
    }
}
