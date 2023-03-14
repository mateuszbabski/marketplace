using Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Persistence
{
    public interface ICustomerRepository
    {
        Customer? GetCustomerByEmail(string email);
        void Add(Customer customer);
    }
}
