using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Domain.Persistence;
using Product.Infrastructure.Persistence;

namespace Product.Infrastructure;

public static class  DependencyInjection
{
  
    public static IServiceCollection AddProductPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ProductContextImp>(option => option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(ProductContextImp).Assembly.FullName)), ServiceLifetime.Transient);

        services.AddScoped<IProductContext, ProductContextImp>();
        return services;
    }
}


