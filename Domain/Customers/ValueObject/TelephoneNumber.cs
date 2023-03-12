using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Customers.ValueObject
{
    public record TelephoneNumber
    {
        public string Number { get; }

        public TelephoneNumber(string number)
        {
            //number only guard
            Number = number;
        }
    }
}
