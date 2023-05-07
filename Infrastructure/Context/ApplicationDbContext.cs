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
    public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get;  set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShopOrder> ShopOrders { get; set; }
        public DbSet<ShopOrderItem> ShopOrderItems { get; set; }
        //public DbSet<Invoice> Invoices { get; set; }
        //public DbSet<ShopInvoice> ShopInvoices { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
