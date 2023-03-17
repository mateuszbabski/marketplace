using Domain.Shared.Exceptions;

namespace Domain.Shared.ValueObjects
{
    public record PasswordHash
    {
        public string Value { get; }

        public PasswordHash(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidPasswordException();
            }
            Value = value;
        }

        public static implicit operator string(PasswordHash passwordHash) => passwordHash.Value;
        public static implicit operator PasswordHash(string value) => new(value);
    }
}
