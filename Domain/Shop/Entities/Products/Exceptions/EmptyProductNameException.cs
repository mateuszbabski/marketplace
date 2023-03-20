using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shop.Entities.Products.Exceptions
{
    public class EmptyProductNameException : Exception
    {
        public EmptyProductNameException() : base(message: "Product name cannot be empty.")
        {
            
        }
    }
}
