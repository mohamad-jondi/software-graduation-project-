namespace Domain.DTOs.Chats
{
    public class CreateChatMessageDTO
    {
        public string MessageContent { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsRead { get; set; }
    }
}