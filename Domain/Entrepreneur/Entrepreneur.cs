using Domain.Entrepreneur.ValueObjects;
using Domain.Shared.ValueObjects;

namespace Domain.Entrepreneur
{
    public class Entrepreneur
    {
        public EntrepreneurId Id { get; private set; }
        public Email Email { get; private set; }
        public PasswordHash PasswordHash { get; private set; }
        public Name Name { get; private set; }
        public LastName LastName { get; private set; }
        public ShopName ShopName { get; private set; }
        public TaxNumber TaxNumber { get; private set; }
        public TelephoneNumber TelephoneNumber { get; private set; }
        public Address ShopAddress { get; private set; }
        public Roles Role { get; private set; } = Roles.entrepreneur;

        internal Entrepreneur(EntrepreneurId id,
                                   Email email,
                                   PasswordHash passwordHash,
                                   Name name,
                                   LastName lastName,
                                   ShopName shopName,
                                   Address shopAddress,
                                   TaxNumber taxNumber,
                                   TelephoneNumber telephoneNumber)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            Name = name;
            LastName = lastName;
            ShopName = shopName;
            ShopAddress = shopAddress;
            TaxNumber = taxNumber;
            TelephoneNumber = telephoneNumber;
            Role = Roles.entrepreneur;
        }
    }
}
