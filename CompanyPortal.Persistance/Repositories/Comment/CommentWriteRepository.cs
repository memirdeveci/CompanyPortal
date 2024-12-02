using CompanyPortal.Application.Abstractions.Repositories.Comment;
using CompanyPortal.Persistance.DbContext;

namespace CompanyPortal.Persistance.Repositories.Comment
{
    public class CommentWriteRepository : WriteRepository<Domain.Entities.Comment>, ICommentWriteRepository
    {
        public CommentWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
