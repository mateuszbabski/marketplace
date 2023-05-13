using Application.Authentication;
using Application.Authentication.Services;
using Application.Common.Behaviors;
using Application.Features.Products.AddProduct;
using Application.Middleware;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddTransient<GlobalExceptionHandlerMiddleware>();

            services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(assembly));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            services.AddValidatorsFromAssembly(assembly);

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IValidator<RegisterCustomerRequest>, RegisterCustomerRequestValidator>();
            services.AddScoped<IValidator<RegisterShopRequest>, RegisterShopRequestValidator>();
            services.AddScoped<IValidator<AddProductCommand>, AddProductCommandValidator>();            

            return services;
        }
    }
}

