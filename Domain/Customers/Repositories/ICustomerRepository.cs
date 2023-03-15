using Domain.Customers;
using Domain.Customers.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> Add(Customer customer);
        Task<Customer?> GetCustomerByEmail(string email);
        Task<Customer?> GetCustomerById(CustomerId id);
    }
}
