using CompanyPortal.Application.Abstractions.Repositories.Chat;
using CompanyPortal.Persistance.DbContext;

namespace CompanyPortal.Persistance.Repositories.Chat
{
    public class ChatWriteRepository : WriteRepository<Domain.Entities.Chat>, IChatWriteRepository
    {
        public ChatWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
