using CompanyPortal.Application.Abstractions.Repositories.Chat;
using CompanyPortal.Application.Abstractions.Repositories.ChatMessage;
using CompanyPortal.Persistance.DbContext;

namespace CompanyPortal.Persistance.Repositories.ChatMessage
{
    public class ChatMessageReadRepository : ReadRepository<Domain.Entities.ChatMessage>, IChatMessageReadRepository
    {
        public ChatMessageReadRepository(AppDbContext context) : base(context)
        {
            
        }
    }
}
