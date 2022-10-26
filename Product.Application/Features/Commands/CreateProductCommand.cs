using MediatR;
using Product.Application.Cache.Redis.Interfaces;
using Product.Application.Cache.Redis.RedisHelper;
using Product.Domain.Entities;
using Product.Domain.Persistence;

namespace Product.Application.Features.Commands;

public class CreateProductCommand : IRequest<int>
{
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public string? Barcode { get; set; }
    public string? Description { get; set; }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductContext _context;
        private readonly ICacheService _cache;
        public CreateProductCommandHandler(IProductContext context,ICacheService cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Products
            {
                Name = request.Name,
                Price = request.Price,
                IsActive = request.IsActive,
                Barcode = request.Barcode,
                Description = request.Description
            };

            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync();
            _cache.RemoveData(GenerateCacheKeys.products);
            return product.Id;
        }

    }
}


