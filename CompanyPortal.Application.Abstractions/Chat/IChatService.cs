using CompanyPortal.Application.Abstractions.Chat.Dtos;
using System.Security.Claims;

namespace CompanyPortal.Application.Abstractions.Chat
{
    public interface IChatService
    {
        Task<bool> AddChat(ChatDto chat, ClaimsPrincipal principal);
        Task<bool> DeleteChat(Guid id);
        Task<bool> EditChat(ChatDto chat);
        Task<ChatDto> GetChatById(Guid id);
        Task<List<ChatDto>> GetAllChat();
    }
}
