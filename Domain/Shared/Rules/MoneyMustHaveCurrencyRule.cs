using Domain.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.Rules
{
    public class MoneyMustHaveCurrencyRule : IBusinessRule
    {
        public string Message => throw new NotImplementedException();

        public bool IsBroken()
        {
            throw new NotImplementedException();
        }
    }
}
