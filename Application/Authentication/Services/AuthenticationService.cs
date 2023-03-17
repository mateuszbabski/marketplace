using Application.Common.Interfaces;
using Domain.Customers.Repositories;
using Domain.Shared.ValueObjects;
using Domain.Customers.Factories;
using Domain.Entrepreneur.Repositories;
using Domain.Entrepreneur.Factories;

namespace Application.Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenManager _tokenManager;
        private readonly IEntrepreneurRepository _entrepreneurRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IHashingService _hashingService;
        private readonly IEntrepreneurFactory _entrepreneurFactory;
        private readonly ICustomerFactory _customerFactory;

        public AuthenticationService(ITokenManager tokenManager,
                                     IEntrepreneurRepository entrepreneurRepository,
                                     ICustomerRepository customerRepository,
                                     IHashingService hashingService,
                                     IEntrepreneurFactory entrepreneurFactory,
                                     ICustomerFactory customerFactory)
        {
            _tokenManager = tokenManager;
            _entrepreneurRepository = entrepreneurRepository;
            _customerRepository = customerRepository;
            _hashingService = hashingService;
            _entrepreneurFactory = entrepreneurFactory;
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

        public async Task<AuthenticationResult> RegisterEntrepreneur(RegisterEntrepreneurRequest request)
        {
            var emailInUse = await _entrepreneurRepository.GetEntrepreneurByEmail(request.Email);

            if (emailInUse != null)
            {
                throw new Exception("Email already taken");
            }

            var passwordHash = _hashingService.GenerateHashPassword(request.Password);
            var shopAddress = new Address(request.Country, request.City, request.Street, request.PostalCode);

            var entrepreneur = _entrepreneurFactory.Create(Guid.NewGuid(),
                                                   request.Email,
                                                   passwordHash,
                                                   request.Name,
                                                   request.LastName,
                                                   request.ShopName,
                                                   shopAddress,
                                                   request.TaxNumber,
                                                   request.TelephoneNumber);

            await _entrepreneurRepository.Add(entrepreneur);

            var token = _tokenManager.GenerateToken(entrepreneur.Id, entrepreneur.Email);

            return new AuthenticationResult(entrepreneur.Id, token);
        }

        public async Task<AuthenticationResult> Login(LoginRequest request)
        {
            var customer = await _customerRepository.GetCustomerByEmail(request.Email) ?? throw new Exception("Email not found");

            if (!_hashingService.ValidatePassword(request.Password, customer.PasswordHash))
            {
                throw new Exception("Invalid password");
            }

            var token = _tokenManager.GenerateToken(customer.Id, customer.Email);

            return new AuthenticationResult(customer.Id, token);
        }
    }
}
