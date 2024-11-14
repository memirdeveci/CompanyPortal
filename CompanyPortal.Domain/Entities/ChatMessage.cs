using CompanyPortal.Domain.Entities.Common;

namespace CompanyPortal.Domain.Entities
{
    public class ChatMessage : BaseEntity
    {
        public string? Text { get; set; }
        public DateTime SendDate { get; set; }

        //Connections
        public Guid ChatId { get; set; }
        public Chat? Chat { get; set; }
        public Guid UserId { get; set; }
        public AppUser? User { get; set; }
    }
}
