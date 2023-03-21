using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.ValueObjects
{
    public record Currency(string Symbol)
    {
        public static Currency PLN => new("PLN");
        public static Currency USD => new("USD");
        public static Currency EUR => new("EUR");
    }
}
