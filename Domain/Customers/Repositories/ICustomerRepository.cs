using Domain.Customers.ValueObjects;

namespace Domain.Customers.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> Add(Customer customer);
        Task<Customer> GetCustomerByEmail(string email);
        Task<Customer> GetCustomerById(CustomerId id);
        Task<Customer> GetCustomerWithCartById(CustomerId id);
    }
}
