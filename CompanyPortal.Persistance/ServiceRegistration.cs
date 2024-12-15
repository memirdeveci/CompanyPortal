using CompanyPortal.Application.Abstractions.Chat;
using CompanyPortal.Application.Abstractions.Comment;
using CompanyPortal.Application.Abstractions.Department;
using CompanyPortal.Application.Abstractions.Like;
using CompanyPortal.Application.Abstractions.Post;
using CompanyPortal.Application.Abstractions.Repositories.Chat;
using CompanyPortal.Application.Abstractions.Repositories.Comment;
using CompanyPortal.Application.Abstractions.Repositories.Department;
using CompanyPortal.Application.Abstractions.Repositories.Like;
using CompanyPortal.Application.Abstractions.Repositories.Post;
using CompanyPortal.Application.Abstractions.User;
using CompanyPortal.Application.Chat;
using CompanyPortal.Application.Comment;
using CompanyPortal.Application.Department;
using CompanyPortal.Application.Like;
using CompanyPortal.Application.Post;
using CompanyPortal.Application.User;
using CompanyPortal.Domain.Entities;
using CompanyPortal.Persistance.DbContext;
using CompanyPortal.Persistance.Repositories.Chat;
using CompanyPortal.Persistance.Repositories.Comment;
using CompanyPortal.Persistance.Repositories.Department;
using CompanyPortal.Persistance.Repositories.Like;
using CompanyPortal.Persistance.Repositories.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyPortal.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddRequiredServices(this IServiceCollection services, ConfigurationManager config)
        {
            var mySql = config.GetConnectionString("MySql") ?? string.Empty;

            services.AddDbContext<AppDbContext>(options => options.UseMySQL(mySql));

            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>();

            services.AddAutoMapper(typeof(Application.Mappings.Profiles));

            //Department
            services.AddScoped<IDepartmentReadRepository, DepartmentReadRepository>();
            services.AddScoped<IDepartmentWriteRepository, DepartmentWriteRepository>();
            services.AddScoped<IDepartmentService, DepartmentService>();

            //Post
            services.AddScoped<IPostReadRepository, PostReadRepository>();
            services.AddScoped<IPostWriteRepository, PostWriteRepository>();
            services.AddScoped<IPostService, PostService>();

            //User-Login
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILoginService, LoginService>();

            //Comment
            services.AddScoped<ICommentReadRepository, CommentReadRepository>();
            services.AddScoped<ICommentWriteRepository, CommentWriteRepository>();
            services.AddScoped<ICommentService, CommentService>();

            //Like
            services.AddScoped<ILikeReadRepository, LikeReadRepository>();
            services.AddScoped<ILikeWriteRepository, LikeWriteRepository>();
            services.AddScoped<ILikeService, LikeService>();

            //Chat
            services.AddScoped<IChatReadRepository, ChatReadRepository>();
            services.AddScoped<IChatWriteRepository, ChatWriteRepository>();
            services.AddScoped<IChatService, ChatService>();
        }
    }
}
