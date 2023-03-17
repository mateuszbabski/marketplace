using Infrastructure.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using Infrastructure.Authentication.Services;
using Application.Common.Interfaces;
using Infrastructure.Services;
using Infrastructure.Authentication;
using Domain.Customers.Repositories;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));         
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IApplicationDbContext>(options => options.GetRequiredService<ApplicationDbContext>());

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<ITokenManager, TokenManager>();
            services.AddSingleton<IHashingService, HashingService>();

            services.AddScoped<ICustomerRepository, CustomerRepository>(); 
            services.AddScoped<IEntrepreneurRepository, EntrepreneurRepository>();

            

            return services;
        }
    }
}
