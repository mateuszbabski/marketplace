using Domain.Customers;
using Domain.Shared.Exceptions;
using Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Domain.Customers
{
    public class CustomerDomainTest
    {
        [Fact]
        public void CreateCustomer_ReturnsCustomerIfParamsAreValid()
        {
            var address = Address.CreateAddress("country", "city", "street", "postalCode");

            var customer = Customer.Create(Guid.NewGuid(),
                                           "customer@example.com",
                                           "passwordHash",
                                           "name",
                                           "lastName",
                                           address,
                                           "telephoneNumber");

            Assert.NotNull(customer);
            Assert.IsType<Customer>(customer);
            Assert.Equal("customer@example.com", customer.Email);
        }

        [Fact]
        public void CreateCustomer_ThrowsInvalidEmailExceptionWhenEmailIsInvalid()
        {
            var address = Address.CreateAddress("country", "city", "street", "postalCode");

            var act = Assert.Throws<InvalidEmailException>(() => Customer.Create(Guid.NewGuid(),
                                           "",
                                           "passwordHash",
                                           "name",
                                           "lastName",
                                           address,
                                           "telephoneNumber"));

            Assert.IsType<InvalidEmailException>(act);
        }

        [Fact]
        public void UpdateCustomerDetails_ReturnsUpdatedCustomerDetailsIfParamsAreValid()
        {
            var customer = GetCustomer();

            customer.UpdateCustomerDetails("updatedName",
                                           null,
                                           null,
                                           null,
                                           null,
                                           null,
                                           null);

            Assert.NotNull(customer);
            Assert.IsType<Customer>(customer);
            Assert.Equal("updatedName", customer.Name);            
        }

        [Fact]
        public void UpdateCustomerDetails_ReturnsOriginalCustomerDataIfParamsAreNull()
        {
            var customer = GetCustomer();

            customer.UpdateCustomerDetails(null,
                                           null,
                                           null,
                                           null,
                                           null,
                                           null,
                                           null);

            Assert.NotNull(customer);
            Assert.IsType<Customer>(customer);
            Assert.Equal("name", customer.Name);
        }

        private static Customer GetCustomer()
        {
            var address = Address.CreateAddress("country", "city", "street", "postalCode");

            var customer = Customer.Create(Guid.NewGuid(),
                                           "customer@example.com",
                                           "passwordHash",
                                           "name",
                                           "lastName",
                                           address,
                                           "telephoneNumber");

            return customer;
        }
    }
}
