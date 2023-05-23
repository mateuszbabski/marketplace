using Application.Common.Behaviors;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Features.Products.AddProduct;
using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.Products;
using Domain.Shops.Entities.Products.Exceptions;
using Domain.Shops.Entities.Products.Repositories;
using Domain.Shops.Repositories;
using Moq;
using UnitTest.Domain.Products;
using UnitTest.Domain.Shops;

namespace UnitTest.Application.Products
{
    public class AddProductTest
    {
        private readonly AddProductCommandHandler _sut;
        private readonly Mock<IProductRepository> _productRepository = new();
        private readonly Mock<IShopRepository> _shopRepository = new();
        private readonly Mock<ICurrentUserService> _userService = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        public AddProductTest()
        {
            _sut = new AddProductCommandHandler(_productRepository.Object,
                                                _shopRepository.Object,
                                                _userService.Object,
                                                _unitOfWork.Object);
        }

        [Fact]
        public async Task AddProduct_ValidField_ReturnsGuid()
        {
            var command = new AddProductCommand()
            {
                ProductName = "Test",
                ProductDescription = "Test",
                Amount = 1,
                Currency = "USD",
                Unit = "KG"
            };

            var shop = ShopFactory.Create();

            var price = MoneyValue.Of(command.Amount, command.Currency);

            var product = shop.AddProduct(command.ProductName,
                                          command.ProductDescription,
                                          price,
                                          command.Unit,
                                          shop.Id);

            _userService.Setup(s => s.UserId).Returns(shop.Id);

            _shopRepository.Setup(s => s.GetShopById(shop.Id)).ReturnsAsync(shop);

            _productRepository.Setup(p => p.Add(product)).ReturnsAsync(product);

            var result = await _sut.Handle(command, CancellationToken.None);

            Assert.IsType<Guid>(result);
        }

        //[Fact]
        //public async Task AddProduct_InvalidField_Throws()
        //{
        //    var command = new AddProductCommand()
        //    {
        //        ProductName = "",
        //        ProductDescription = "Test",
        //        Amount = 1,
        //        Currency = "USD",
        //        Unit = "KG"
        //    };

        //    var shop = ShopFactory.Create();

        //    var price = MoneyValue.Of(command.Amount, command.Currency);

        //    var product = shop.AddProduct(command.ProductName,
        //                                  command.ProductDescription,
        //                                  price,
        //                                  command.Unit,
        //                                  shop.Id);

        //    _userService.Setup(s => s.UserId).Returns(shop.Id);

        //    _shopRepository.Setup(s => s.GetShopById(shop.Id)).ReturnsAsync(shop);

        //    //_productRepository.Setup(p => p.Add(product)).ThrowsAsync(new EmptyProductNameException());

        //    var result = await Assert.ThrowsAsync<>(()
        //        => _sut.Handle(command, CancellationToken.None));

        //    //Assert.IsType<EmptyProductNameException>(result);
        //    Assert.Fail("Product name cannot be empty.");
        //    //Assert.Equal("Product name cannot be empty.", result.Message);
        //}
    }
}
