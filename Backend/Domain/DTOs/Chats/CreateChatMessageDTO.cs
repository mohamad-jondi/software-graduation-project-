namespace Domain.DTOs.Chats
{
    public class CreateChatMessageDTO
    {
        public DateTime SentDateTime { get; set; }
        public string MessageContent { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsRead { get; set; }
    }
}