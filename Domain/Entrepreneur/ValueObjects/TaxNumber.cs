using Domain.Entrepreneur.Exceptions;

namespace Domain.Entrepreneur.ValueObjects
{
    public record TaxNumber
    {
        public string Value { get; }

        public TaxNumber(string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                throw new EmptyTaxNumberException();
            }

            Value = value;
        }

        public static implicit operator string(TaxNumber taxNumber) => taxNumber.Value;
        public static implicit operator TaxNumber(string value) => new(value);
    }
}
