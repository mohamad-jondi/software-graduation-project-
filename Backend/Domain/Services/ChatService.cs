using Domain.IServices;
using Data.Interfaces;
using Data.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Domain.DTOs.Chats;

namespace Domain.Services
{
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ChatService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

       public async Task<IEnumerable<ChatForShowingFromTheOutsideDTO>> GetChatsByUserUsername(string username)
{
    var userChats = await _unitOfWork.GetRepositories<Chat>()
        .Get()
        .Include(c => c.Messages)
        .Include(c => c.SecondParty)
        .Include(c => c.FirstParty)
        .Where(c => c.FirstParty.Username == username || c.SecondParty.Username == username)
        .ToListAsync();

    var chatDtos = new List<ChatForShowingFromTheOutsideDTO>();

    foreach (var chat in userChats)
    {
        var lastMessage = chat.Messages.OrderByDescending(m => m.SentDateTime).FirstOrDefault();

        // New logic to count unread messages until the first read message
        var sortedMessages = chat.Messages.OrderByDescending(m => m.SentDateTime).ToList();
        int unreadMessagesCount = 0;

        foreach (var message in sortedMessages)
        {
            if (message.IsRead)
            {
                break;
            }

            if (!message.IsRead && message.SenderUsername != username)
            {
                unreadMessagesCount++;
            }
        }

        var chatDto = new ChatForShowingFromTheOutsideDTO
        {
            ChatId = chat.ChatID,
            FirstPartyUserName = chat.FirstParty.Username,
            SecondPartyUsername = chat.SecondParty.Username,
            IsTheLastSenderMe = lastMessage != null && lastMessage.SenderUsername == username,
            lastSentMassagess = lastMessage?.MessageContent,
            LastMessageDate = lastMessage.SentDateTime,
            numberOfMessages = unreadMessagesCount
        };

        chatDtos.Add(chatDto);
    }

    return chatDtos;
}


        public async Task<IEnumerable< ChatMessageDTO>> BrowseChat(int ChatID)
        {
            var chat = await _unitOfWork.GetRepositories<Chat>()
                .Get()
                .Include(c => c.Messages)
                .Include(c => c.FirstParty)
                .Include(c => c.SecondParty)
                .FirstOrDefaultAsync(c => c.ChatID == ChatID);

            var lastMessage = chat?.Messages.OrderByDescending(m => m.SentDateTime).ToList();
            return _mapper.Map<IEnumerable<ChatMessageDTO>>(lastMessage);
        }

        public async Task<ChatMessageDTO> SendMessage(string senderUsername, string receiverUsername, CreateChatMessageDTO chatMessageDTO)
        {
            var chat = await _unitOfWork.GetRepositories<Chat>()
                .Get()
                .Include(c => c.Messages)
                .Include(c => c.FirstParty)
                .Include(c => c.SecondParty)
                .FirstOrDefaultAsync(c => (c.FirstParty.Username == senderUsername && c.SecondParty.Username == receiverUsername) ||
                                           (c.FirstParty.Username == receiverUsername && c.SecondParty.Username == senderUsername));

            if (chat == null)
            {
                var firstParty = await _unitOfWork.GetRepositories<User>().Get().FirstOrDefaultAsync(p => p.Username == senderUsername );
                var secondParty = await _unitOfWork.GetRepositories<User>().Get().FirstOrDefaultAsync(d => d.Username == receiverUsername);
                if (firstParty == null || secondParty == null) return null;
                chat = new Chat
                {
                    FirstParty = firstParty,
                    SecondParty = secondParty,
                    Messages = new List<ChatMessage>()
                };
                chat = await _unitOfWork.GetRepositories<Chat>().Add(chat);
            }

            var chatMessage = _mapper.Map<ChatMessage>(chatMessageDTO);
            chatMessage.SenderUsername = senderUsername;
            chatMessage.SentDateTime = DateTime.UtcNow;
            chat.Messages.Add(chatMessage);
            await _unitOfWork.GetRepositories<Chat>().Update(chat);

            return _mapper.Map<ChatMessageDTO>(chatMessage);
        }

        public async Task<bool> DeleteMessage(int messageId)
        {
            var message = await _unitOfWork.GetRepositories<ChatMessage>().Get().FirstOrDefaultAsync(m => m.ChatMessageID == messageId);
            if (message == null) return false;

            await _unitOfWork.GetRepositories<ChatMessage>().Delete(message);
            return true;
        }

        public async Task<bool> SetChatAsRead(int chatId)
        {
            var chat = await _unitOfWork.GetRepositories<ChatMessage>()
                .Get()
                .FirstOrDefaultAsync(c => c.ChatMessageID == chatId);

            if (chat == null)
            {
                throw new Exception("Chat not found");
            }

            chat.IsRead = true;
           await _unitOfWork.GetRepositories<ChatMessage>().Update(chat);
            return true;
        }
    }
}
