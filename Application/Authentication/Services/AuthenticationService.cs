using Application.Common.Interfaces;
using Application.Common.Persistence;
using Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenManager _tokenManager;
        private readonly ICustomerRepository _customerRepository;

        public AuthenticationService(ITokenManager tokenManager, ICustomerRepository customerRepository)
        {
            _tokenManager = tokenManager;
            _customerRepository = customerRepository;
        }

        //public AuthenticationResult RegisterCustomer(RegisterCustomerRequest request) 
        //{
        //if(_customerRepository.GetCustomerByEmail(email) is not null)
        //{
        //    throw new Exception("Email already taken");
        //}

        //var customer = new Customer { };

        //_customerRepository.Add(customer);

        //var token = _tokenManager.GenerateToken(customer.Id, email);

        //return new AuthenticationResult(customer.Id, token);
        //}

        //public AuthenticationResult Login(string email, string password)
        //{
        //    if (_customerRepository.GetCustomerByEmail(email) is not Customer customer)
        //    {
        //        throw new Exception("Email not found");
        //    }

        //    if (customer.passwordhash != passwordhash)
        //    {
        //        throw new Exception("Invalid password");
        //    }

        //    var token = _tokenManager.GenerateToken(customer.Id, email);

        //    return new AuthenticationResult(customer.Id, token);
        //}
    }
}
