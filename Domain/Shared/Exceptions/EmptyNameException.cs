using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.Exceptions
{
    public class EmptyNameException : Exception
    {
        public EmptyNameException() : base(message: "Name or LastName field cannot be empty.")
        {
        }
    }
}
