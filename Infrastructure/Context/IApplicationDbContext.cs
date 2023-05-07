using Domain.Customers;
using Domain.Customers.Entities.Orders;
using Domain.Customers.Entities.ShoppingCarts;
using Domain.Invoices;
using Domain.Shops;
using Domain.Shops.Entities.Products;
using Domain.Shops.Entities.ShopOrders;
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
        DbSet<Order> Orders { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }
        DbSet<ShopOrder> ShopOrders { get; set; }
        DbSet<ShopOrderItem> ShopOrderItems { get; set; }
        DbSet<Invoice> Invoices { get; set; }
        DbSet<ShopInvoice> ShopInvoices { get; set; }

        Task<int> SaveChangesAsync();
    }
}