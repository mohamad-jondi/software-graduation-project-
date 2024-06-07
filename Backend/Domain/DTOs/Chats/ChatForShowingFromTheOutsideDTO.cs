namespace Domain.DTOs.Chats
{
    public class ChatForShowingFromTheOutsideDTO
    {
        public int ChatId { get; set; }
        public string FirstPartyUserName { get; set; }
        public string SecondPartyUsername { get; set; }
        public bool IsTheLastSenderMe { get; set; }
        public string lastSentMassagess { get; set; }
        public int numberOfMessages { get; set; }
    }
}
