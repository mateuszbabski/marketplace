using Domain.Shop.Entities.Products.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shop.Entities.Products.ValueObjects
{
    public record ProductDescription
    {
        public string Value { get; }
        public ProductDescription(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new EmptyProductDescriptionException();
            }

            Value = value;
        }

        public static implicit operator string(ProductDescription productDescription) => productDescription.Value;
        public static implicit operator ProductDescription(string value) => new(value);
    }
}
