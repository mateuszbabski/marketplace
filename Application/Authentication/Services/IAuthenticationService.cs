using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Services
{
    public interface IAuthenticationService
    {
        //public AuthenticationResult Login(string email, string password);
        public Task<AuthenticationResult> RegisterCustomer(RegisterCustomerRequest request);
        //public AuthenticationResult RegisterEnterpreneur();
    }
}
