using Auth.Domain.Persistence;
using Auth.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Infrastructure;
public static class DependencyInjection
{

    public static IServiceCollection AddProductPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AuthContextImp>(option => option.UseSqlServer(configuration.GetConnectionString("AuthConnection"),
            b => b.MigrationsAssembly(typeof(AuthContextImp).Assembly.FullName)), ServiceLifetime.Transient);

        services.AddScoped<IAuthContext, AuthContextImp>();
        return services;
    }
}
