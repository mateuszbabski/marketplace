using Domain.Shared.Exceptions;

namespace Domain.Shared.ValueObjects
{
    public record TelephoneNumber
    {
        public string Value { get; }

        public TelephoneNumber(string value)
        {
            //number only guard
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidTelephoneNumberException();
            }
            Value = value;
        }

        public static implicit operator string(TelephoneNumber telephoneNumber) => telephoneNumber.Value;
        public static implicit operator TelephoneNumber(string value) => new(value);
    }
}
