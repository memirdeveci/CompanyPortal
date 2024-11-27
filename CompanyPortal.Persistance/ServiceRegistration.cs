using CompanyPortal.Application.Abstractions.Department;
using CompanyPortal.Application.Abstractions.Repositories.Department;
using CompanyPortal.Application.Department;
using CompanyPortal.Domain.Entities;
using CompanyPortal.Persistance.DbContext;
using CompanyPortal.Persistance.Repositories.Department;
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

            services.AddScoped<IDepartmentReadRepository, DepartmentReadRepository>();
            services.AddScoped<IDepartmentWriteRepository, DepartmentWriteRepository>();
            services.AddScoped<IDepartmentService, DepartmentService>();
        }
    }
}
