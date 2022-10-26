using MediatR;
using Product.Application.Cache.Redis.Interfaces;
using Product.Application.Services.ProductService.Interfaces;
using Product.Domain.Entities;
using Product.Domain.Persistence;

namespace Product.Application.Features.Queries;

public class GetAllProductQuery : IRequest<IEnumerable<Products>>
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, IEnumerable<Products>>
    {
        private readonly IProductService _product;
        public GetAllProductQueryHandler(IProductService product)
        {
            _product = product;
        }
        public async Task<IEnumerable<Products>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var products = await _product.GetProductsFromCacheIsNoThenSetAsync(cancellationToken);
            return products;
        }
    }
}
