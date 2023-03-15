using Application.Common.Interfaces;
using Domain.Customers.Repositories;
using Domain.Customers;
using Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Customers.Factories;

namespace Application.Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenManager _tokenManager;
        private readonly ICustomerRepository _customerRepository;
        private readonly IHashingService _hashingService;
        private readonly ICustomerFactory _customerFactory;

        public AuthenticationService(ITokenManager tokenManager, ICustomerRepository customerRepository, IHashingService hashingService, ICustomerFactory customerFactory)
        {
            _tokenManager = tokenManager;
            _customerRepository = customerRepository;
            _hashingService = hashingService;
            _customerFactory = customerFactory;
        }

        public async Task<AuthenticationResult> RegisterCustomer(RegisterCustomerRequest request)
        {
            var emailInUse = await _customerRepository.GetCustomerByEmail(request.Email);

            if (emailInUse != null)
            {
                throw new Exception("Email already taken");
            }
            
            var passwordHash = _hashingService.GenerateHashPassword(request.Password);
            var address = new Address(request.Country, request.City, request.Street, request.PostalCode);

            var customer = _customerFactory.Create(Guid.NewGuid(),
                                                   request.Email,
                                                   passwordHash,
                                                   request.Name,
                                                   request.LastName,
                                                   address,
                                                   request.TelephoneNumber);

            await _customerRepository.Add(customer);

            var token = _tokenManager.GenerateToken(customer.Id, customer.Email);

            return new AuthenticationResult(customer.Id, token);
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
