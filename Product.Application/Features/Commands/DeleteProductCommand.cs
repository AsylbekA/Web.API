using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Application.Cache.Redis.Interfaces;
using Product.Application.Cache.Redis.RedisHelper;
using Product.Domain.Persistence;

namespace Product.Application.Features.Commands;

public class DeleteProductCommand : IRequest<int>
{
    public int Id { get; set; }
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
    {
        private readonly IProductContext _context;
        private readonly ICacheService _cache;

        public DeleteProductCommandHandler(IProductContext context, ICacheService cache)
        {
            _context = context;
            _cache = cache;
        }
        public async Task<int> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _context.Products.Where(p => p.Id == command.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken: cancellationToken);
            if (product is null) return default;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            _cache.RemoveData(GenerateCacheKeys.products);
            return product.Id;
        }
    }
}
