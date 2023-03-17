using Domain.Shared.Exceptions;

namespace Domain.Shared.ValueObjects
{
    public record Email
    {
        public string Value { get; }

        public Email(string value)
        {
            //email regex validation
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidEmailException();
            }

            Value = value;
        }
        public static implicit operator string(Email email) => email.Value;
        public static implicit operator Email(string value) => new(value);
    }

}
