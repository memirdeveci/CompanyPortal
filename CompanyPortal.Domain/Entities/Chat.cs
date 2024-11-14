using CompanyPortal.Domain.Entities.Common;

namespace CompanyPortal.Domain.Entities
{
    public class Chat : BaseEntity
    {
        public string? ChatName { get; set; }
        public string? ChatPhoto { get; set; }

        //Connections
        public ICollection<AppUser>? Users { get; set; }
        public ICollection<ChatMessage>? Messages { get; set; }
    }
}
