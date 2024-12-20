using CompanyPortal.Application.Abstractions.Chat.Dtos;
using CompanyPortal.Domain.Entities;

namespace CompanyPortal.Models
{
    public class ChatModel
    {
        public List<ChatDto> Chats { get; set; } = new();
        public AppUser User { get; set; }
    }
}
