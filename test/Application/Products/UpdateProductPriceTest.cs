using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Features.Products.ChangeProductAvailability;
using Application.Features.Products.UpdateProduct;
using Application.Features.Products.UpdateProductPrice;
using Domain.Shops.Entities.Products.Exceptions;
using Domain.Shops.Entities.Products.Repositories;
using Domain.Shops.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.Domain.Shops;

namespace UnitTest.Application.Products
{
    public class UpdateProductPriceTest
    {
        private readonly UpdateProductPriceCommandHandler _sut;
        private readonly Mock<IProductRepository> _productRepository = new();
        private readonly Mock<IShopRepository> _shopRepository = new();
        private readonly Mock<ICurrentUserService> _userService = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        public UpdateProductPriceTest()
        {
            _sut = new UpdateProductPriceCommandHandler(_userService.Object,
                                                        _productRepository.Object,
                                                        _shopRepository.Object,
                                                        _unitOfWork.Object);
        }

        [Fact]
        public async Task ChangeProductPrice_ChangesPriceIfValidMoneyValue()
        {
            var shop = ShopFactory.Create();

            var productList = new ProductList(shop);

            var command = new UpdateProductPriceCommand()
            {
                Id = productList.Products[0].Id,
                Amount = 5,
                Currency = "USD"
                
            };

            _userService.Setup(s => s.UserId).Returns(shop.Id);

            _shopRepository.Setup(s => s.GetShopById(shop.Id)).ReturnsAsync(shop);

            _productRepository.Setup(p => p.GetById(command.Id)).ReturnsAsync(productList.Products[0]);

            Assert.Equal(10, productList.Products[0].Price.Amount);

            await _sut.Handle(command, CancellationToken.None);

            Assert.Equal(5, productList.Products[0].Price.Amount);            
        }

        [Fact]
        public async Task ChangeProductPrice_ThrowsNotFoundIfIdIsInvalid()
        {
            var shop = ShopFactory.Create();

            var productList = new ProductList(shop);

            var command = new UpdateProductPriceCommand()
            {
                Id = Guid.NewGuid(),
                Amount = 5,
                Currency = "USD"
            };

            _userService.Setup(s => s.UserId).Returns(shop.Id);

            _shopRepository.Setup(s => s.GetShopById(shop.Id)).ReturnsAsync(shop);

            var result = await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task ChangeProductPrice_ThrowsIfAmountIsZero()
        {
            var shop = ShopFactory.Create();

            var productList = new ProductList(shop);

            var command = new UpdateProductPriceCommand()
            {
                Id = productList.Products[0].Id,
                Amount = 0,
                Currency = "USD"
            };

            _userService.Setup(s => s.UserId).Returns(shop.Id);

            _shopRepository.Setup(s => s.GetShopById(shop.Id)).ReturnsAsync(shop);

            _productRepository.Setup(p => p.GetById(command.Id)).ReturnsAsync(productList.Products[0]);

            var result = await Assert.ThrowsAsync<InvalidProductPriceException>(() => _sut.Handle(command, CancellationToken.None));
            Assert.Equal("Money amount value cannot be zero or negative.", result.Message);
        }

        [Fact]
        public async Task ChangeProductPrice_ThrowsIfCurrencyIsNotAvailable()
        {
            var shop = ShopFactory.Create();

            var productList = new ProductList(shop);

            var command = new UpdateProductPriceCommand()
            {
                Id = productList.Products[0].Id,
                Amount = 5,
                Currency = "PES"
            };

            _userService.Setup(s => s.UserId).Returns(shop.Id);

            _shopRepository.Setup(s => s.GetShopById(shop.Id)).ReturnsAsync(shop);

            _productRepository.Setup(p => p.GetById(command.Id)).ReturnsAsync(productList.Products[0]);

            var result = await Assert.ThrowsAsync<InvalidProductPriceException>(() => _sut.Handle(command, CancellationToken.None));
            Assert.Equal("Invalid currency.", result.Message);
        }
    }
}
