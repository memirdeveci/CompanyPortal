using CompanyPortal.Application.Abstractions.Repositories.Chat;
using CompanyPortal.Persistance.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyPortal.Persistance.Repositories.Chat
{
    public class ChatReadRepository : ReadRepository<Domain.Entities.Chat>, IChatReadRepository
    {
        public ChatReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
