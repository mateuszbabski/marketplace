using Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    internal interface IApplicationDbContext
    {
        DbSet<Customer> Customers { get; set; }

        Task<int> SaveChangesAsync();
    }
}