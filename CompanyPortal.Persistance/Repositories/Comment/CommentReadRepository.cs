using CompanyPortal.Application.Abstractions.Repositories.Comment;
using CompanyPortal.Persistance.DbContext;

namespace CompanyPortal.Persistance.Repositories.Comment
{
    public class CommentReadRepository : ReadRepository<Domain.Entities.Comment>, ICommentReadRepository
    {
        public CommentReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
