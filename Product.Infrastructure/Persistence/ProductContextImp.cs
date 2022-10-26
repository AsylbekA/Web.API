using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Domain.Persistence;

namespace Product.Infrastructure.Persistence;

public class ProductContextImp:DbContext,IProductContext
{
    #region Constructor
    public ProductContextImp(DbContextOptions<ProductContextImp> options) : base(options) { }
    #endregion

    #region DbSet
   public DbSet<Products> Products { get; set; }
    #endregion

    #region Methods
    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
    #endregion
}

