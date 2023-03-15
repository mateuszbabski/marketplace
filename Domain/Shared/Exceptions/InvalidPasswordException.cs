using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException() : base(message: "Invalid password")
        {
        }
    }
}
