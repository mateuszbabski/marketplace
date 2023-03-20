using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shop.Entities.Products.Exceptions
{
    public class InvalidProductPriceException : Exception
    {
        public InvalidProductPriceException() : base(message: "Invalid product price.")
        {
            
        }
    }
}
