using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.Exceptions
{
    public class InvalidUnitException : Exception
    {
        public InvalidUnitException() : base(message: "Invalid unit.")
        {
            
        }
    }
}
