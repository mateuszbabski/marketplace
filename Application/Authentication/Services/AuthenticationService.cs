using Application.Common.Interfaces;
using Application.Common.Persistence;
using Domain.Customers;
using Domain.Customers.ValueObject;
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
        private readonly IHashingService _hashingService;

        public AuthenticationService(ITokenManager tokenManager, ICustomerRepository customerRepository, IHashingService hashingService)
        {
            _tokenManager = tokenManager;
            _customerRepository = customerRepository;
            _hashingService = hashingService;
        }

        public async Task<AuthenticationResult> RegisterCustomer(RegisterCustomerRequest request)
        {
            if (_customerRepository.GetCustomerByEmail(request.Email) is not null)
            {
                throw new Exception("Email already taken");
            }
            
            var passwordHash = _hashingService.GenerateHashPassword(request.Password);

            var customer = Customer.CreateRegistered(request.Email,
                                                     passwordHash,
                                                     request.Name,
                                                     request.LastName,
                                                     request.Country,
                                                     request.City,
                                                     request.Street,
                                                     request.PostalCode,
                                                     request.TelephoneNumber);

            await _customerRepository.Add(customer);

            var token = _tokenManager.GenerateToken(customer.Id.Value, customer.Email.Value);

            return new AuthenticationResult(customer.Id.Value, token);
        }

        //public AuthenticationResult Login(string email, string password)
        //{
        //    //    if (_customerRepository.GetCustomerByEmail(email) is not Customer customer)
        //    //    {
        //    //        throw new Exception("Email not found");
        //    //    }

        //    //    if (customer.passwordhash != passwordhash)
        //    //    {
        //    //        throw new Exception("Invalid password");
        //    //    }

        //    //    var token = _tokenManager.GenerateToken(customer.Id, email);

        //    //    return new AuthenticationResult(customer.Id, token);
        //    return email;

        //}
    }
}
