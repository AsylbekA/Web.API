using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Application.Cache.Redis.Interfaces;
using Product.Application.Cache.Redis.RedisHelper;
using Product.Domain.Persistence;

namespace Product.Application.Features.Commands;

public class UpdateProductCommand : IRequest<int>
{
    public int Id { get; private set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public string? Barcode { get; set; }
    public string? Description { get; set; }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
    {
        private readonly IProductContext _context;
        private readonly ICacheService _cache;

        public UpdateProductCommandHandler(IProductContext context, ICacheService cache)
        {
            _context = context;
            _cache = cache;
        }
        public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _context.Products.Where(p => p.Id == request.Id).AsNoTracking().FirstOrDefault();

            if (product is null) return default;

            product.Name = request.Name;
            product.Price = request.Price;
            product.IsActive = request.IsActive;
            product.Barcode = request.Barcode;
            product.Description = request.Description;

            await _context.SaveChangesAsync();
            _cache.RemoveData(GenerateCacheKeys.products);
            return product.Id;
        }
    }
}