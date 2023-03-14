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

        public static implicit operator string(Name name) => name.Value;
        public static implicit operator Name(string value) => new(value);
    }
}
