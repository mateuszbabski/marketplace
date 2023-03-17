using Domain.Shared.Exceptions;

namespace Domain.Shared.ValueObjects
{
    public record LastName
    {
        public string Value { get; }

        public LastName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new EmptyNameException();
            }

            Value = value;
        }

        public static implicit operator string(LastName lastName) => lastName.Value;
        public static implicit operator LastName(string value) => new(value);
    }
}
