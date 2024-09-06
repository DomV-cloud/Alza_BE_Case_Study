using Application.Interfaces.Authentication;
using Application.Interfaces.Provider;
using Application.Interfaces.Repository.CustomerRepository;
using Application.Interfaces.Repository.ItemRepository;
using Application.Interfaces.Repository.OrderRepository;
using Domain.Entities;
using Infrastructure.Authentication;
using Infrastructure.Persistence.Repository;
using Infrastructure.Persistence.Repository.CustomerRepository;
using Infrastructure.Persistence.Repository.OrderRepository;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
           this IServiceCollection services,
           ConfigurationManager configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            return services;
        }
    }
}
