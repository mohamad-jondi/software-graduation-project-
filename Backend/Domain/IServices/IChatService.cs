using Domain.DTOs.Chats;

namespace Domain.IServices
{
    public interface IChatService
    {
        Task<IEnumerable<ChatForShowingFromTheOutsideDTO>> GetChatsByUserUsername(string username);
        Task<IEnumerable<ChatMessageDTO>> BrowseChat(int ChatID);
        Task<ChatMessageDTO> SendMessage(string senderUsername,string ReciverUsername, CreateChatMessageDTO chat);
        Task<bool> DeleteMessage(int messageId);
    }
}
