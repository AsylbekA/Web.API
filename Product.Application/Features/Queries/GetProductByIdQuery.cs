using MediatR;
using Product.Application.Services.ProductService.Interfaces;
using Product.Domain.Entities;

namespace Product.Application.Features.Queries;

public class GetProductByIdQuery : IRequest<Products>
{
    public int Id { get; set; }
    public class GetProductByIdQueryHandle : IRequestHandler<GetProductByIdQuery, Products>
    {
        private readonly IProductService _product;
        public GetProductByIdQueryHandle(IProductService product)
        {
            _product = product;
        }

        public async Task<Products> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Products product;
            var products = await _product.GetProductsFromCacheIsNoThenSetAsync(cancellationToken);
            product = products.FirstOrDefault(x => x.Id == request.Id);
            if (product is null) return null;
            return product;

        }
    }
}
