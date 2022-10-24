using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Domain.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Persistence;

public class ProductContextImp:DbContext,IProductContext
{
    #region Constructor
    public ProductContextImp(DbContextOptions<ProductContextImp> options) : base(options) { }
    #endregion

    #region DbSet
   //public DbSet<Product> Products { get; set; }
    #endregion

    #region Methods
    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
    #endregion
}

