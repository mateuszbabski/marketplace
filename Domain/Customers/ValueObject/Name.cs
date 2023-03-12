using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.ValueObject
{
    public record Name
    {
        public string Value { get; }

        public Name(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new EmptyNameException();
            }

            Value = value;
        }
    }
}
