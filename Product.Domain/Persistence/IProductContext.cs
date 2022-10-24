using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Product.Domain.Entities;

namespace Product.Domain.Persistence
{
    public interface IProductContext
    {
        //DbSet<Product> Products { get; set; }

        Task<int> SaveChangesAsync();
    }
}
