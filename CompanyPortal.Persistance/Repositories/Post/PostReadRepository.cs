using CompanyPortal.Application.Abstractions.Repositories.Post;
using CompanyPortal.Persistance.DbContext;

namespace CompanyPortal.Persistance.Repositories.Post
{
    public class PostReadRepository : ReadRepository<Domain.Entities.Post>, IPostReadRepository
    {
        public PostReadRepository(AppDbContext context) : base(context) { }
    }
}
