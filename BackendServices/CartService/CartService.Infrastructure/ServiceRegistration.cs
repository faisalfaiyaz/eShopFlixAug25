using CartService.Application.Mappers;
using CartService.Application.Repositories;
using CartService.Application.Services.Abstractions;
using CartService.Application.Services.Implementations;
using CartService.Infrastructure.Persistence;
using CartService.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CartService.Infrastructure;

public class ServiceRegistration
{
    public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CartServiceDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DbConnection")));

        services.AddScoped<ICartAppService, CartAppService>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddAutoMapper(cfg => cfg.AddProfile<CartMapper>());
    }
}
