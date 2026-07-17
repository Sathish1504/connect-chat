using Identity.Application.Interfaces;
using Identity.Infrastructure.Authentication;
using Identity.Infrastructure.CurrentUser;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Identity.Infrastructure.Email;
using Identity.Application.Interfaces.Security;
using Identity.Infrastructure.Security;

namespace Identity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));


        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddJwtAuthentication(configuration);
        services.AddScoped<IEmailService, ConsoleEmailService>();

        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

        return services;
    }
}