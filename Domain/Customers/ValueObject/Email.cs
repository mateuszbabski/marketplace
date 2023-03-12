using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.ValueObject
{
    public record Email
    {
        public string Value { get; }

        public Email(string value)
        {
            //email regex validation
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidEmailException();
            }

            Value = value;
        }
    }
}
