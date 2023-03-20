using Domain.Shared.ValueObjects;
using Domain.Shop.Entities.Products.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shop.Entities.Products.ValueObjects
{
    public class ProductPrice
    {
        public MoneyValue Value { get; private set; }
        public ProductPrice(MoneyValue value)
        {
            if(Value == null)
            {
                throw new InvalidProductPriceException();
            }

            Value = value;
        }
    }
}
