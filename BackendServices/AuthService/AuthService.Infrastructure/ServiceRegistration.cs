using AuthService.Application.Mapper;
using AuthService.Application.Repositories;
using AuthService.Application.Services.Abstractions;
using AuthService.Application.Services.Implementations;
using AuthService.Infrastructure.Persistence;
using AuthService.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Infrastructure;

public static class ServiceRegistration
{
    public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        // Dbcontext registration
        services.AddDbContext<AuthServiceDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DbConnection")));

        // Register repositories
        services.AddScoped<IUserRepository, UserRepository>();

        // Register application services
        services.AddScoped<IUserAppService, UserAppService>();

        // Register AutoMapper
        services.AddAutoMapper(cfg => cfg.AddProfile<AuthMapper>());
    }
}
