using Domain.Shop.Entities.Products.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shop.Entities.Products.ValueObjects
{
    public record ProductName
    {
        public string Value { get; }
        public ProductName(string value)
        {
            if(string.IsNullOrEmpty(value))
            {
                throw new EmptyProductNameException();
            }

            Value = value;
        }

        public static implicit operator string(ProductName productName)  => productName.Value;
        public static implicit operator ProductName(string value) => new(value);
    }
}
