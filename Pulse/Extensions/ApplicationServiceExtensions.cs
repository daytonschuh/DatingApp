using BusinessLogicLayer;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer;
using Services;

namespace Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<MapperClass>();
            services.AddScoped<Repository>();
            services.AddScoped<BusinessLogicClass>();
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}