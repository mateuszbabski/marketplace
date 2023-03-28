using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shops.Entities.Products.Exceptions
{
    public class EmptyProductDescriptionException : Exception
    {
        public EmptyProductDescriptionException() : base(message: "Product description cannot be empty.")
        {
            
        }
    }
}
