using Product.Domain.Entities;

namespace Product.Application.Services.ProductService.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Products>> GetProductsFromCacheIsNoThenSetAsync(CancellationToken cancellationToken);
    }
}
