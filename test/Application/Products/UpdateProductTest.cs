using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Features.Products.UpdateProduct;
using Domain.Shops.Entities.Products.Repositories;
using Domain.Shops.Repositories;
using Moq;
using UnitTest.Domain.Shops;

namespace UnitTest.Application.Products
{
    public class UpdateProductTest
    {
        private readonly UpdateProductCommandHandler _sut;
        private readonly Mock<IProductRepository> _productRepository = new();
        private readonly Mock<IShopRepository> _shopRepository = new();
        private readonly Mock<ICurrentUserService> _userService = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        public UpdateProductTest()
        {
            _sut = new UpdateProductCommandHandler(_productRepository.Object,
                                                   _userService.Object,
                                                   _shopRepository.Object,
                                                   _unitOfWork.Object);
        }

        [Fact]
        public async Task UpdateProduct_UpdatesProductIfValidParams()
        {
            var shop = ShopFactory.Create();

            var productList = new ProductList(shop);

            var command = new UpdateProductCommand()
            {
                Id = productList.Products[0].Id,
                ProductName = "Updated name",
                ProductDescription = "Updated description",
                Unit = "pcs"
            };

            _userService.Setup(s => s.UserId).Returns(shop.Id);

            _shopRepository.Setup(s => s.GetShopById(shop.Id)).ReturnsAsync(shop);

            _productRepository.Setup(p => p.GetById(command.Id)).ReturnsAsync(productList.Products[0]);

            await _sut.Handle(command, CancellationToken.None);

            _unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
            Assert.Equal("Updated name", productList.Products[0].ProductName);
            Assert.Equal("Updated description", productList.Products[0].ProductDescription);
            Assert.Equal("pcs", productList.Products[0].Unit);
        }

        [Fact]
        public async Task UpdateProduct_DoNothingIfParamsAreEmpty()
        {
            var shop = ShopFactory.Create();

            var productList = new ProductList(shop);

            var command = new UpdateProductCommand()
            {
                Id = productList.Products[0].Id
            };

            _userService.Setup(s => s.UserId).Returns(shop.Id);

            _shopRepository.Setup(s => s.GetShopById(shop.Id)).ReturnsAsync(shop);

            _productRepository.Setup(p => p.GetById(command.Id)).ReturnsAsync(productList.Products[0]);

            await _sut.Handle(command, CancellationToken.None);

            Assert.Equal("productName", productList.Products[0].ProductName);
        }

        [Fact]
        public async Task UpdateProduct_ThrowsNotFoundIfIdIsInvalid()
        {
            var shop = ShopFactory.Create();

            var productList = new ProductList(shop);

            var command = new UpdateProductCommand()
            {
                Id = Guid.NewGuid()
            };

            _userService.Setup(s => s.UserId).Returns(shop.Id);

            _shopRepository.Setup(s => s.GetShopById(shop.Id)).ReturnsAsync(shop);

            _userService.Setup(s => s.UserId).Returns(shop.Id);

            _shopRepository.Setup(s => s.GetShopById(shop.Id)).ReturnsAsync(shop);

            var result = await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));
        }
    }
}

