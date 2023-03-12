using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException() : base(message: "Invalid email.")
        {
        }
    }
}
