using Microsoft.Extensions.DependencyInjection;
using Product.Application.Cache.Redis;
using Product.Application.Cache.Redis.Interfaces;
using Product.Application.Services;
using Product.Application.Services.ProductService.Interfaces;

namespace Product.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProductPersistenceApplication(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductServiceImp>();
            services.AddScoped<ICacheService, CacheService>();
            return services;
        }
    }
}
