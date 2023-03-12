using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.ValueObject
{
    public record PasswordHash
    {
        public string Value { get; }

        public PasswordHash(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidPasswordException();
            }
            Value = value;
        }
    }
}
