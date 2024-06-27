using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Picture : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }
        public string Url { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
