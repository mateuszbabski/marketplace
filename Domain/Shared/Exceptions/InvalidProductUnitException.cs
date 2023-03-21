using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.Exceptions
{
    public class InvalidProductUnitException : Exception
    {
        public InvalidProductUnitException() : base(message: "Invalid unit.")
        {
            
        }
    }
}
