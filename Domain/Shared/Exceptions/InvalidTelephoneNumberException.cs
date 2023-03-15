using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.Exceptions
{
    public class InvalidTelephoneNumberException : Exception
    {
        public InvalidTelephoneNumberException() : base(message: "Invalid number.")
        {
        }
    }
}
