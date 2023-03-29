using Domain.Customers;
using Domain.Shops;
using Domain.Shops.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    internal sealed class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get;  set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Products> Products { get; set; }

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
