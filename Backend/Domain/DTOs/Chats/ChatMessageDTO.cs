namespace Domain.DTOs.Chats
{
    public class ChatMessageDTO
    {
        public int ChatMessageID { get; set; }
        public string SenderUsername { get; set; }
        public DateTime SentDateTime { get; set; }
        public string MessageContent { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsRead { get; set; }
    }
}
