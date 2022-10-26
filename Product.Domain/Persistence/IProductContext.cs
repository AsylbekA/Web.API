using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;

namespace Product.Domain.Persistence;

public interface IProductContext
{
    DbSet<Products> Products { get; set; }

    Task<int> SaveChangesAsync();
}
