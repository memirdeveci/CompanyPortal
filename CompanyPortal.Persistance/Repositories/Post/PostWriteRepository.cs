using CompanyPortal.Application.Abstractions.Repositories.Post;
using CompanyPortal.Persistance.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyPortal.Persistance.Repositories.Post
{
    public class PostWriteRepository : WriteRepository<Domain.Entities.Post>, IPostWriteRepository
    {
        public PostWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
