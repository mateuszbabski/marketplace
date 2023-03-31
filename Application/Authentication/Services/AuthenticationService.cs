using Application.Common.Interfaces;
using Domain.Customers.Repositories;
using Domain.Shared.ValueObjects;
using Domain.Shops.Repositories;
using Domain.Shops;
using FluentValidation;
using Domain.Customers;

namespace Application.Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenManager _tokenManager;
        private readonly IShopRepository _shopRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IHashingService _hashingService;

        public AuthenticationService(ITokenManager tokenManager,
                                     IShopRepository shopRepository,
                                     ICustomerRepository customerRepository,
                                     IHashingService hashingService)
        {
            _tokenManager = tokenManager;
            _shopRepository = shopRepository;
            _customerRepository = customerRepository;
            _hashingService = hashingService;
        }

        public async Task<AuthenticationResult> RegisterCustomer(RegisterCustomerRequest request)
        {            
            var validator = new RegisterCustomerRequestValidator();
            validator.ValidateAndThrow(request);

            await CheckIfEmailIsFreeToUse(request.Email);

            var passwordHash = _hashingService.GenerateHashPassword(request.Password);
            var address = Address.CreateAddress(request.Country, request.City, request.Street, request.PostalCode);

            var customer = Customer.Create(Guid.NewGuid(),
                                                   request.Email,
                                                   passwordHash,
                                                   request.Name,
                                                   request.LastName,
                                                   address,
                                                   request.TelephoneNumber);

            await _customerRepository.Add(customer);

            var token = _tokenManager.GenerateToken(customer.Id, customer.Email, customer.Role);

            return new AuthenticationResult(customer.Id, token);
        }

        public async Task<AuthenticationResult> RegisterShop(RegisterShopRequest request)
        {
            var validator = new RegisterShopRequestValidator();
            validator.ValidateAndThrow(request);

            await CheckIfEmailIsFreeToUse(request.Email);

            var passwordHash = _hashingService.GenerateHashPassword(request.Password);
            var shopAddress = Address.CreateAddress(request.Country, request.City, request.Street, request.PostalCode);
            
            var shop = Shop.Create(Guid.NewGuid(),
                                                   request.Email,
                                                   passwordHash,
                                                   request.OwnerName,
                                                   request.OwnerLastName,
                                                   request.ShopName,
                                                   shopAddress,
                                                   request.TaxNumber,
                                                   request.ContactNumber);

            await _shopRepository.Add(shop);

            var token = _tokenManager.GenerateToken(shop.Id, shop.Email, shop.Role);

            return new AuthenticationResult(shop.Id, token);
        }

        public async Task<AuthenticationResult> LoginCustomer(LoginRequest request)
        {
            var customer = await _customerRepository.GetCustomerByEmail(request.Email) ?? throw new Exception("Email not found");            

            if (!_hashingService.ValidatePassword(request.Password, customer.PasswordHash))
            {
                throw new Exception("Invalid password");
            }

            var token = _tokenManager.GenerateToken(customer.Id, customer.Email, customer.Role);

            return new AuthenticationResult(customer.Id, token);
        }

        public async Task<AuthenticationResult> LoginShop(LoginRequest request)
        {                        
            var shop = await _shopRepository.GetShopByEmail(request.Email) ?? throw new Exception("Email not found");            

            if (!_hashingService.ValidatePassword(request.Password, shop.PasswordHash))
            {
                throw new Exception("Invalid password");
            }

            var token = _tokenManager.GenerateToken(shop.Id, shop.Email, shop.Role);

            return new AuthenticationResult(shop.Id, token);
        }

        private async Task<bool> CheckIfEmailIsFreeToUse(string email)
        {
            var customer = await _customerRepository.GetCustomerByEmail(email);
            var shop = await _shopRepository.GetShopByEmail(email);
            if (customer != null || shop != null)
                throw new Exception("Email cannot be used");
            return true;
        }
        
    }
}
