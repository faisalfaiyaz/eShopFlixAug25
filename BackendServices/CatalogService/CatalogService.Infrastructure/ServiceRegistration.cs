using CatalogService.Application.Mappers;
using CatalogService.Application.Repositories;
using CatalogService.Application.Services.Abstractions;
using CatalogService.Application.Services.Implementations;
using CatalogService.Infrastructure.Persistence;
using CatalogService.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.Infrastructure;

public static class ServiceRegistration
{
    public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        // Dbcontext registration
        services.AddDbContext<CatalogServiceDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DbConnection")));

        // Register repositories
        services.AddScoped<IProductRepository, ProductRepository>();

        // Register application services
        services.AddScoped<IProductAppService, ProductAppService>();

        // Register AutoMapper
        services.AddAutoMapper(cfg => cfg.AddProfile<ProductMapper>());
    }
}
