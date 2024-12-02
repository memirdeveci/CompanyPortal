using CompanyPortal.Application.Abstractions.Repositories.Like;
using CompanyPortal.Persistance.DbContext;

namespace CompanyPortal.Persistance.Repositories.Like
{
    public class LikeWriteRepository : WriteRepository<Domain.Entities.Like>, ILikeWriteRepository
    {
        public LikeWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
