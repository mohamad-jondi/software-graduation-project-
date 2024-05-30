using Data.enums;
namespace Domain.DTOs
{
    public class ChatMessageDTO
    {
        public int ChatMessageID { get; set; }
        public PersonType Sender { get; set; }
        public DateTime SentDateTime { get; set; }
        public string MessageContent { get; set; }
    }
}
