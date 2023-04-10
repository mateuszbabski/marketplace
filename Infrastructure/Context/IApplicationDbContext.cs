using Domain.Customers;
using Domain.Customers.Entities.ShoppingCarts;
using Domain.Shops;
using Domain.Shops.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public interface IApplicationDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Shop> Shops { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ShoppingCart> ShoppingCarts { get; set; }
        DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        Task<int> SaveChangesAsync();
    }
}