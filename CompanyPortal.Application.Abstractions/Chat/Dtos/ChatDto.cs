using CompanyPortal.Application.Abstractions.Base;

namespace CompanyPortal.Application.Abstractions.Chat.Dtos
{
    public class ChatDto : BaseDto
    {
        public string? ChatName { get; set; }
        public string? ChatPhoto { get; set; }
        public List<string>? UserIds { get; set; }

        //Connections
        public ICollection<Domain.Entities.AppUser>? Users { get; set; }
        public ICollection<Domain.Entities.ChatMessage>? Messages { get; set; }
    }
}
