using Microsoft.EntityFrameworkCore;
using Product.Application.Cache.Redis.Interfaces;
using Product.Application.Cache.Redis.RedisHelper;
using Product.Application.Services.ProductService.Interfaces;
using Product.Domain.Entities;
using Product.Domain.Persistence;

namespace Product.Application.Services;

internal class ProductServiceImp: IProductService
{
    private readonly IProductContext _context;
    private readonly ICacheService _cache;

    public ProductServiceImp(IProductContext context, ICacheService cache)
    {
        _context = context;
        _cache = cache;
    }
    public async Task<IEnumerable<Products>> GetProductsFromCacheIsNoThenSetAsync(CancellationToken cancellationToken)
    {
        var products = _cache.GetData<IEnumerable<Products>>(GenerateCacheKeys.products);
        if (products != null) return products;
        products = await _context.Products.ToListAsync(cancellationToken);
        if (products is null) return null;

        DateTimeOffset expirationTime = DateTimeOffset.Now.AddMinutes(15.0);
        _cache.SetData(GenerateCacheKeys.products, products, expirationTime);
        return products;
    }
}
