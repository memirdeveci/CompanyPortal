using CompanyPortal.Application.Abstractions.Base;

namespace CompanyPortal.Application.Abstractions.ChatMessage.Dtos
{
    public class ChatMessageDto : BaseDto
    {
        public string? Text { get; set; }
        public DateTime? SendDate { get; set; } = DateTime.Now;
        public bool IsOwnMessage { get; set; } = false;

        //Connections
        public Guid ChatId { get; set; }
        public Domain.Entities.Chat? Chat { get; set; }
        public Guid UserId { get; set; }
        public Domain.Entities.AppUser? User { get; set; }
    }
}
