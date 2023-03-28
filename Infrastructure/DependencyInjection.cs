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
using Domain.Shops.Repositories;
using Domain.Shops.Entities.Products.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.GetSection("JwtSettings").Bind(jwtSettings);
            services.AddSingleton(jwtSettings);
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IApplicationDbContext>(options => options.GetRequiredService<ApplicationDbContext>());

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<ITokenManager, TokenManager>();
            services.AddSingleton<IHashingService, HashingService>();

            services.AddScoped<ICustomerRepository, CustomerRepository>(); 
            services.AddScoped<IShopRepository, ShopRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
                        
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                    };
                });

            return services;
        }
    }
}
