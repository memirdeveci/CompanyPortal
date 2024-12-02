using CompanyPortal.Application.Abstractions.Repositories.Like;
using CompanyPortal.Persistance.DbContext;

namespace CompanyPortal.Persistance.Repositories.Like
{
    internal class LikeReadRepository : ReadRepository<Domain.Entities.Like>, ILikeReadRepository
    {
        public LikeReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
