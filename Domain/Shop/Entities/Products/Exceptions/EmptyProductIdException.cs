using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shop.Entities.Products.Exceptions
{
    public class EmptyProductIdException : Exception
    {
        public EmptyProductIdException() : base(message: "Product Id cannot be empty.")
        {
            
        }
    }
}
