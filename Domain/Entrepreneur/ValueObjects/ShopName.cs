using Domain.Entrepreneur.Exceptions;

namespace Domain.Entrepreneur.ValueObjects
{
    public record ShopName
    {
        public string Value { get; }

        public ShopName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new EmptyShopNameException();
            }

            Value = value;
        }

        public static implicit operator string(ShopName shopName) => shopName.Value;
        public static implicit operator ShopName(string value) => new(value);
    }
}
