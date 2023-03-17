using Domain.Entrepreneur.Exceptions;

namespace Domain.Entrepreneur.ValueObjects
{
    public record EntrepreneurId
    {
        public Guid Value { get; }

        public EntrepreneurId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyEntrepreneurIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(EntrepreneurId id) => id.Value;
        public static implicit operator EntrepreneurId(Guid value) => new(value);
    }
}
