using Domain.Shops.Entities.Products.Exceptions;
using Domain.Shops.Exceptions;
using Domain.Shops.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shops.Entities.Products.ValueObjects
{
    public record ProductId
    {
        public Guid Value { get; }

        public ProductId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new EmptyProductIdException();
            }

            Value = value;
        }

        public static implicit operator Guid(ProductId id) => id.Value;
        public static implicit operator ProductId(Guid value) => new(value);
    }
}
