using Domain.Customers;
using Domain.Shop;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    internal interface IApplicationDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Shop> Shops { get; set; }

        Task<int> SaveChangesAsync();
    }
}