using Microsoft.EntityFrameworkCore;
using MYTBlazorTraining.WebApi.Db;
using MYTBlazorTraining.WebApi.Repository;
using MYTBlazorTraining.WebApi.Services;

namespace MYTBlazorTraining.WebApi
{
    public static class ModularServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
            services.AddRepository();
            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DbConnection")!;
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString),
                ServiceLifetime.Transient,
                ServiceLifetime.Transient
            );
            return services;
        }

        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IBlog, BlogRepo>();
            services.AddScoped<IAuth, AuthRepo>();
            return services;

        }
    }
}
