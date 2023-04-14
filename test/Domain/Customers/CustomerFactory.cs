using Domain.Customers;
using Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Domain.Customers
{
    public class CustomerFactory
    {
        public static Customer GetCustomer()
        {
            var address = Address.CreateAddress("country", "city", "street", "postalCode");

            var customer = Customer.Create("customer@example.com",
                                           "passwordHash",
                                           "name",
                                           "lastName",
                                           address,
                                           "telephoneNumber");

            return customer;
        }
    }
}
