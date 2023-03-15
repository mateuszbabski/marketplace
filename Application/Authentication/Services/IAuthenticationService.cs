using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Services
{
    public interface IAuthenticationService
    {
        //AuthenticationResult Login(string email, string password);
        Task<AuthenticationResult> RegisterCustomer(RegisterCustomerRequest request);
        //AuthenticationResult RegisterEnterpreneur();
    }
}
