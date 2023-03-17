using Domain.Customers;
using Domain.Entrepreneur;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    internal interface IApplicationDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Entrepreneur> Entrepreneurs { get; set; }

        Task<int> SaveChangesAsync();
    }
}