using Domain.Customers;
using Domain.Shops;
using Domain.Shops.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public interface IApplicationDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Shop> Shops { get; set; }
        DbSet<Products> Products { get; set; }

        Task<int> SaveChangesAsync();
    }
}