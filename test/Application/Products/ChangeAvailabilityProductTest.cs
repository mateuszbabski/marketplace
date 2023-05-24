using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Features.Products.ChangeProductAvailability;
using Domain.Shops.Entities.Products.Repositories;
using Domain.Shops.Repositories;
using Moq;
using UnitTest.Domain.Shops;

namespace UnitTest.Application.Products
{
    public class ChangeAvailabilityProductTest
    {
        private readonly ChangeProductAvailabilityCommandHandler _sut;
        private readonly Mock<IProductRepository> _productRepository = new();
        private readonly Mock<IShopRepository> _shopRepository = new();
        private readonly Mock<ICurrentUserService> _userService = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        public ChangeAvailabilityProductTest()
        {
            _sut = new ChangeProductAvailabilityCommandHandler(_productRepository.Object,
                                                               _userService.Object,
                                                               _shopRepository.Object,
                                                               _unitOfWork.Object);
        }

        [Fact]
        public async Task ChangeProductAvailability_ChangesAvailability()
        {
            var shop = ShopFactory.Create();

            var productList = new ProductList(shop);

            var command = new ChangeProductAvailabilityCommand()
            {
                Id = productList.Products[0].Id,
            };

            _userService.Setup(s => s.UserId).Returns(shop.Id);

            _shopRepository.Setup(s => s.GetShopById(shop.Id)).ReturnsAsync(shop);

            _productRepository.Setup(p => p.GetById(command.Id)).ReturnsAsync(productList.Products[0]);

            Assert.True(productList.Products[0].IsAvailable);

            await _sut.Handle(command, CancellationToken.None);

            Assert.False(productList.Products[0].IsAvailable);
        }

        [Fact]
        public async Task ChangeProductAvailability_ThrowsNotFoundIfIdIsInvalid()
        {
            var shop = ShopFactory.Create();

            var productList = new ProductList(shop);

            var command = new ChangeProductAvailabilityCommand()
            {
                Id = Guid.NewGuid(),
            };

            _userService.Setup(s => s.UserId).Returns(shop.Id);

            _shopRepository.Setup(s => s.GetShopById(shop.Id)).ReturnsAsync(shop);

            var result = await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));
        }
    }
}
