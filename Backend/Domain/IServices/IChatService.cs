using Domain.DTOs;

namespace Domain.IServices
{
    public interface IChatService
    {
        Task<IEnumerable<ChatForShowingFromTheOutsideDTO>> GetChatsByUserUsername(string username);
        Task<ChatMessageDTO> BrowesMessage(string senderUsername, string ReciverUsernames);
        Task<ChatMessageDTO> SendMessage(string senderUsername,string ReciverUsername, ChatMessageDTO chat);
        Task<bool> DeleteMessage(int messageId);
    }
}
