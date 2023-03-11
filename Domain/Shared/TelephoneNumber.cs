using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared
{
    public record TelephoneNumber
    {
        public string Number { get; }

        public TelephoneNumber(string number) 
        {
            Number = number;
        }
    }
}
