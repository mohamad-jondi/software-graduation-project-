using Data.enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class ChatMessage : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChatMessageID { get; set; }
        [ForeignKey("ChatID")] 
        
        public int ChatID { get; set; }
        public Chat? Chat { get; set; }
        public PersonType Sender { get; set; }
        public DateTime SentDateTime { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsRead { get; set; }
        public string MessageContent { get; set; }
    }
}
