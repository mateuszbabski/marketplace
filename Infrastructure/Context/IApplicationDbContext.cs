using Domain.Customers;
using Domain.Shop;
using Domain.Shop.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public interface IApplicationDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Shop> Shops { get; set; }
        DbSet<Product> Products { get; set; }

        Task<int> SaveChangesAsync();
    }
}