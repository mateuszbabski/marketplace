using Application.Authentication;
using Application.Authentication.Services;
using Application.Features.Products.AddProduct;
using Domain.Customers.Factories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(assembly));

            services.AddValidatorsFromAssembly(assembly);

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ICustomerFactory, CustomerFactory>();

            services.AddScoped<IValidator<RegisterCustomerRequest>, RegisterCustomerRequestValidator>();
            services.AddScoped<IValidator<RegisterShopRequest>, RegisterShopRequestValidator>();
            services.AddScoped<IValidator<AddProductCommand>, AddProductCommandValidator>();            

            return services;
        }
    }
}

