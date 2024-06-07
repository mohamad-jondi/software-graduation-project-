using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Chat : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChatID { get; set; }
        [ForeignKey("FirstPartyID")]
        public int FirstPartyID { get; set; }
        public User FirstParty { get; set; }
        [ForeignKey("SecondPartyID")]
        public int SecondPartyID { get; set; }
        public User? SecondParty { get; set; } 
        public ICollection<ChatMessage> Messages { get; set; } 
    }

}