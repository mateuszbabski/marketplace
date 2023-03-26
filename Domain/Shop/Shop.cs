using Domain.Shop.ValueObjects;
using Domain.Shared.ValueObjects;
using Domain.Shop.Entities.Products;
using Domain.Shop.Entities.Products.Repositories;

namespace Domain.Shop
{
    public class Shop
    {
        public ShopId Id { get; private set; }
        public Email Email { get; private set; }
        public PasswordHash PasswordHash { get; private set; }
        public Name OwnerName { get; private set; }
        public LastName OwnerLastName { get; private set; }
        public ShopName ShopName { get; private set; }
        public TaxNumber TaxNumber { get; private set; }
        public TelephoneNumber ContactNumber { get; private set; }
        public Address ShopAddress { get; private set; }
        public Roles Role { get; private set; } = Roles.shop;
        public List<Product> Products { get; private set; }

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
            Products = new List<Product>();
        }

        public void SetAddress(Address shopAddress)
        {
            ShopAddress = shopAddress;
        }

        public void SetContactNumber(string contactNumber)
        {
            if (string.IsNullOrEmpty(contactNumber))
            {
                ContactNumber = ContactNumber;
            }
            else
            {
                ContactNumber = new TelephoneNumber(contactNumber);
            }            
        }

        public void SetTaxNumber(string taxNumber)
        {
            if (string.IsNullOrEmpty(taxNumber))
            {
                TaxNumber = TaxNumber;
            }
            else
            {
                TaxNumber = new TaxNumber(taxNumber);
            }
        }

        public void SetShopName(string shopName)
        {
            if (string.IsNullOrEmpty(shopName))
            {
                ShopName = ShopName;
            }
            else
            {
                ShopName = new ShopName(shopName);
            }
        }

        public void SetOwnerName(string ownerName)
        {
            if (string.IsNullOrEmpty(ownerName))
            {
                OwnerName = OwnerName;
            }
            else
            {
                OwnerName = new Name(ownerName);
            }
        }

        public void SetOwnerLastName(string ownerLastName)
        {
            if (string.IsNullOrEmpty(ownerLastName))
            {
                OwnerLastName = OwnerLastName;
            }
            else
            {
                OwnerLastName = new LastName(ownerLastName);
            }
        }

        public List<Product> ShowProducts()
        {
            return Products;
        }
    }
}
