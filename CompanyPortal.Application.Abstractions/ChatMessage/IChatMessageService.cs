using CompanyPortal.Application.Abstractions.ChatMessage.Dtos;
using System.Security.Claims;

namespace CompanyPortal.Application.Abstractions.ChatMessage
{
    public interface IChatMessageService
    {
        Task<bool> AddMessage(ChatMessageDto chatMessage);
        Task<List<ChatMessageDto>> GetAllChatMessages(Guid chatId, ClaimsPrincipal principal);
    }
}
