namespace Domain.DTOs
{
    public class ChatDTO
    {
        public ICollection<ChatMessageDTO> Messages { get; set; }
    }
}
