﻿using Application.Common.Interfaces;
using Domain.Customers.Repositories;
using Domain.Shared.ValueObjects;
using Domain.Customers.Factories;
using Domain.Shop.Repositories;
using Domain.Shop.Factories;
using Domain.Customers;
using Domain.Shop;

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
            await CheckIfEmailIsFreeToUse(request.Email);

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

            var token = _tokenManager.GenerateToken(customer.Id, customer.Email, customer.Role);

            return new AuthenticationResult(customer.Id, token);
        }

        public async Task<AuthenticationResult> RegisterShop(RegisterShopRequest request)
        {
            await CheckIfEmailIsFreeToUse(request.Email);

            var passwordHash = _hashingService.GenerateHashPassword(request.Password);
            var shopAddress = new Address(request.Country, request.City, request.Street, request.PostalCode);

            var shop = _shopFactory.Create(Guid.NewGuid(),
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
