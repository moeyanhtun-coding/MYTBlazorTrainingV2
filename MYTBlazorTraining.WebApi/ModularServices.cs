using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MYTBlazorTraining.WebApi.Db;
using MYTBlazorTraining.WebApi.Repository;
using MYTBlazorTraining.WebApi.Services;
using System.Text;

namespace MYTBlazorTraining.WebApi
{
    public static class ModularServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration,
            WebApplicationBuilder builder)
        {
            services.AddDbContext(configuration);
            services.AddRepository();
            services.AddAuthentication(builder, configuration);
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

        public static IServiceCollection AddAuthentication(this IServiceCollection services,
          WebApplicationBuilder builder, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["JWT:Issuer"],
                ValidAudience = builder.Configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!))
            });
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
