using Application.Common.Interfaces;
using Domain.Customers.Repositories;
using Domain.Shared.ValueObjects;
using Domain.Customers.Factories;
using Domain.Shop.Repositories;
using Domain.Shop.Factories;

namespace Application.Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenManager _tokenManager;
        private readonly IShopRepository _shopRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IHashingService _hashingService;
        private readonly IShopFactory _shopFactory;
        private readonly ICustomerFactory _customerFactory;

        public AuthenticationService(ITokenManager tokenManager,
                                     IShopRepository shopRepository,
                                     ICustomerRepository customerRepository,
                                     IHashingService hashingService,
                                     IShopFactory shopFactory,
                                     ICustomerFactory customerFactory)
        {
            _tokenManager = tokenManager;
            _shopRepository = shopRepository;
            _customerRepository = customerRepository;
            _hashingService = hashingService;
            _shopFactory = shopFactory;
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

        public async Task<AuthenticationResult> RegisterShop(RegisterShopRequest request)
        {
            var emailInUse = await _shopRepository.GetShopByEmail(request.Email);

            if (emailInUse != null)
            {
                throw new Exception("Email already taken");
            }

            var passwordHash = _hashingService.GenerateHashPassword(request.Password);
            var shopAddress = new Address(request.Country, request.City, request.Street, request.PostalCode);

            var shop = _shopFactory.Create(Guid.NewGuid(),
                                                   request.Email,
                                                   passwordHash,
                                                   request.Name,
                                                   request.LastName,
                                                   request.ShopName,
                                                   shopAddress,
                                                   request.TaxNumber,
                                                   request.TelephoneNumber);

            await _shopRepository.Add(shop);

            var token = _tokenManager.GenerateToken(shop.Id, shop.Email);

            return new AuthenticationResult(shop.Id, token);
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
