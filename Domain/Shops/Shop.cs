using Domain.Shops.ValueObjects;
using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.Products;
using Domain.Shops.Entities.Products.ValueObjects;
using Domain.Shared.Abstractions;
using Domain.Shared.Rules;

namespace Domain.Shops
{
    public class Shop : IAggregateRoot
    {
        public ShopId Id { get; private set; }
        public Email Email { get; private set; }
        public PasswordHash PasswordHash { get; private set; }
        public Name OwnerName { get; private set; }
        public LastName OwnerLastName { get; private set; }
        public ShopName ShopName { get; private set; }
        public Address ShopAddress { get; private set; }
        public TaxNumber TaxNumber { get; private set; }
        public TelephoneNumber ContactNumber { get; private set; }
        public Roles Role { get; private set; } = Roles.shop;

        public List<Product> ProductList { get; private set; }

        private Shop() { }
        internal Shop(ShopId id,
                                   Email email,
                                   PasswordHash passwordHash,
                                   Name ownerName,
                                   LastName ownerLastName,
                                   ShopName shopName,
                                   Address shopAddress,
                                   TaxNumber taxNumber,
                                   TelephoneNumber contactNumber)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            OwnerName = ownerName;
            OwnerLastName = ownerLastName;
            ShopName = shopName;
            ShopAddress = shopAddress;
            TaxNumber = taxNumber;
            ContactNumber = contactNumber;
            Role = Roles.shop;
            ProductList = new List<Product>();
        }

        public static Shop Create(ShopId id,
                                      Email email,
                                      PasswordHash passwordHash,
                                      Name ownerName,
                                      LastName ownerLastName,
                                      ShopName shopName,
                                      Address shopAddress,
                                      TaxNumber taxNumber,
                                      TelephoneNumber contactNumber)
        {           

            return new Shop(id, email, passwordHash, ownerName, ownerLastName, shopName, shopAddress, taxNumber, contactNumber);
        }

        public void UpdateShopDetails(string ownerName,
                                   string ownerLastName,
                                   string shopName,
                                   string country,
                                   string city,
                                   string street,
                                   string postalCode,
                                   string taxNumber,
                                   string contactNumber)
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

            SetShopName(shopName);
            SetOwnerLastName(ownerLastName);
            SetOwnerName(ownerName);
            SetContactNumber(contactNumber);
            SetTaxNumber(taxNumber);
        }

        internal void SetAddress(Address shopAddress)
        {
            if (shopAddress is not null)
                ShopAddress = shopAddress;
        }

        internal void SetContactNumber(string contactNumber)
        {
            if(!string.IsNullOrEmpty(contactNumber))
                ContactNumber = new TelephoneNumber(contactNumber);           
        }

        internal void SetTaxNumber(string taxNumber)
        {
            if(!string.IsNullOrEmpty(taxNumber))          
                TaxNumber = new TaxNumber(taxNumber);
        }   
        internal void SetShopName(string shopName)
        {
            if (!string.IsNullOrEmpty(shopName))
                ShopName = new ShopName(shopName);
        }

        internal void SetOwnerName(string ownerName)
        {
            if (!string.IsNullOrEmpty(ownerName))
                OwnerName = new Name(ownerName);
        }

        internal void SetOwnerLastName(string ownerLastName)
        {
            if (!string.IsNullOrEmpty(ownerLastName))
                OwnerLastName = new LastName(ownerLastName);
        }

        public string ShowShopAddress()
        {
            return ShopAddress.ToString();
        }

        public Product AddProduct(ProductId id,
                         ProductName productName,
                         ProductDescription productDescription,
                         MoneyValue price,
                         ProductUnit unit,
                         ShopId shopId)
        {
            var product = Product.CreateProduct(id, productName, productDescription, price, unit, shopId);
            
            return product;
        }

        public void ChangeProductPrice(ProductId id, decimal amount, string currency)
        {
            var product = ProductList.Single(x => x.Id == id);

            product.SetPrice(MoneyValue.Of(amount, currency));            
        }

        public void ChangeProductDetails(ProductId id,
                         string productName,
                         string productDescription,
                         string unit)
        {
            var product = ProductList.Single(x => x.Id == id);

            product.ChangeDetails(productName, productDescription, unit);
        }

        public void ChangeProductAvailability(ProductId id)
        {
            var product = ProductList.Single(x => x.Id == id);

            product.ChangeAvailability();
        }

        public void RemoveProduct(ProductId id)
        {
            var product = ProductList.Single(x => x.Id == id);

            product.Remove();
        }
    }
}
