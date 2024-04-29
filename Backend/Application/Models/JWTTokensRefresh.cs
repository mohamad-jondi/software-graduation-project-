using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class JWTTokensRefresh: BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JWTTokensRefreshID { get; set; }
        public Guid RefreshToken { get; set; }
        [ForeignKey("UserID")]
        public int UserID { get; set; }
        public User? user { get; set; } 
    }
}
