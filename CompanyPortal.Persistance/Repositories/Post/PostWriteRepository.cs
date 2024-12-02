using CompanyPortal.Application.Abstractions.Repositories.Post;
using CompanyPortal.Persistance.DbContext;

namespace CompanyPortal.Persistance.Repositories.Post
{
    public class PostWriteRepository : WriteRepository<Domain.Entities.Post>, IPostWriteRepository
    {
        public PostWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
