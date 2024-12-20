using CompanyPortal.Application.Abstractions.Repositories.ChatMessage;
using CompanyPortal.Persistance.DbContext;

namespace CompanyPortal.Persistance.Repositories.ChatMessage
{
    public class ChatMessageWriteRepository : WriteRepository<Domain.Entities.ChatMessage>, IChatMessageWriteRepository
    {
        public ChatMessageWriteRepository(AppDbContext context) : base(context)
        {

        }
    }
}
