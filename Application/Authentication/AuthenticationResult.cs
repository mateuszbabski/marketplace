using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication
{
    public record AuthenticationResult(
         Guid Id,
         string Token);
    
}
