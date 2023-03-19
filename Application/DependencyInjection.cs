﻿using Application.Authentication;
using Application.Authentication.Services;
using Domain.Customers.Factories;
using Domain.Shop.Factories;
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
            services.AddScoped<IShopFactory, ShopFactory>();

            services.AddScoped<IValidator<RegisterCustomerRequest>, RegisterCustomerRequestValidator>();
            services.AddScoped<IValidator<RegisterShopRequest>, RegisterShopRequestValidator>();

            return services;
        }
    }
}

