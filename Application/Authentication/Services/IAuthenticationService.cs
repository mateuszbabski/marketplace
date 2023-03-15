using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Services
{
    public interface IAuthenticationService
    {        
        Task<AuthenticationResult> Login(LoginRequest request);
        Task<AuthenticationResult> RegisterCustomer(RegisterCustomerRequest request);
        //AuthenticationResult RegisterEnterpreneur();
    }
}
