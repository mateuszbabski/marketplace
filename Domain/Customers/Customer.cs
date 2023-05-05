using Domain.Customers.Entities.Orders;
using Domain.Customers.Entities.Orders.ValueObjects;
using Domain.Customers.Entities.ShoppingCarts;
using Domain.Customers.ValueObjects;
using Domain.Shared.Abstractions;
using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.ShopOrders;
using System.Linq;

namespace Domain.Customers
{
    public class Customer : IAggregateRoot
    {
        public CustomerId Id { get; private set; }
        public Email Email { get; private set; }
        public PasswordHash PasswordHash { get; private set; }
        public Name Name { get; private set; }
        public LastName LastName { get; private set; }
        public Address Address { get; private set; }
        public TelephoneNumber TelephoneNumber { get; private set; }

        public Roles Role { get; private set; } = Roles.customer;

        public ShoppingCart ShoppingCart { get; private set; }  
        public List<Order> Orders { get; private set; }
                
        private Customer() 
        {
            Orders = new List<Order>();
        }
        internal Customer(Email email,
                          PasswordHash passwordHash,
                          Name name,
                          LastName lastName,
                          Address address,
                          TelephoneNumber telephoneNumber)
        {
            Id = new CustomerId(Guid.NewGuid());
            Email = email;
            PasswordHash = passwordHash;
            Name = name;
            LastName = lastName;
            Address = address;
            TelephoneNumber = telephoneNumber;
            Orders = new List<Order>();
            Role = Roles.customer;
        }

        public static Customer Create(Email email,
                                      PasswordHash passwordHash,
                                      Name name,
                                      LastName lastName,
                                      Address address,
                                      TelephoneNumber telephoneNumber)
        {
            return new Customer(email, passwordHash, name, lastName, address, telephoneNumber);
        }

        public void UpdateCustomerDetails(string name,
                                          string lastName,
                                          string country,
                                          string city,
                                          string street,
                                          string postalCode,
                                          string telephoneNumber)
        {
            var addressParams = new List<string>()
            {
                country, city, street, postalCode
            };

            if (addressParams.All(c => !string.IsNullOrEmpty(c)))
            {
                var newShopAddress = Address.CreateAddress(country, city, street, postalCode);
                SetAddress(newShopAddress);
            }

            SetName(name);
            SetLastName(lastName);
            SetTelephoneNumber(telephoneNumber);
        }

        public ShoppingCart CreateCart(CustomerId CustomerId)
        {
            var newCart = ShoppingCart.CreateShoppingCart(CustomerId);

            ShoppingCart = newCart;

            return ShoppingCart;
        }

        public ShoppingCart GetShoppingCart()
        {
            return ShoppingCart;
        }

        public Order PlaceOrder(ShoppingCart shoppingCart, Address shippingAddress, DateTime placedOn)
        {
            var order = Order.CreateNew(shoppingCart, shippingAddress, placedOn);

            this.Orders.Add(order);           
            
            return order;
        }

        public void CancelOrder(OrderId orderId)
        {
            var order = this.Orders.Single(x => x.Id == orderId);

            order.CancelOrder();
        }

        internal void SetAddress(Address address)
        {
            if (address is not null)
                Address = address;
        }

        internal void SetName(string name)
        {
            if (!string.IsNullOrEmpty(name))
                Name = new Name(name);
        }

        internal void SetLastName(string lastName)
        {
            if (!string.IsNullOrEmpty(lastName))
                LastName = new LastName(lastName);
        }

        internal void SetTelephoneNumber(string telephoneNumber)
        {
            if (!string.IsNullOrEmpty(telephoneNumber))
                TelephoneNumber = new TelephoneNumber(telephoneNumber);
        }

        public string ShowCustomerAddress()
        {
            return Address.ToString();
        }
    }
}
