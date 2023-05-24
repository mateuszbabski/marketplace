using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Features.ShoppingCarts.AddProductToShoppingCart;
using Domain.Customers.Entities.ShoppingCarts;
using Domain.Customers.Entities.ShoppingCarts.Repositories;
using Domain.Shops.Entities.Products.Repositories;
using Moq;
using UnitTest.Application.Products;
using UnitTest.Domain.Customers;

namespace UnitTest.Application.ShoppingCarts
{
    public class AddProductToShoppingCartTest
    {
        private readonly AddProductToShoppingCartCommandHandler _sut;
        private readonly Mock<ICurrentUserService> _userService = new();
        private readonly Mock<IShoppingCartRepository> _shoppingCartRepository = new ();
        private readonly Mock<IProductRepository> _productRepository =new();
        private readonly Mock<IUnitOfWork> _unitOfWork =new();
        private readonly Mock<ICurrencyConverter> _currencyConverter =new();
        public AddProductToShoppingCartTest()
        {
            _sut = new AddProductToShoppingCartCommandHandler(_userService.Object,
                                                              _shoppingCartRepository.Object,
                                                              _productRepository.Object,
                                                              _unitOfWork.Object,
                                                              _currencyConverter.Object);
        }

        [Fact]
        public async void AddProductToRepository_ReturnsGuid_IfSuccess()
        {
            var customer = CustomerFactory.GetCustomer();
            var productList = new ProductList();

            var command = new AddProductToShoppingCartCommand()
            {
                ProductId = productList.Products.First().Id,
                Quantity = 1
            };

            var shoppingCart = ShoppingCart.CreateShoppingCart(customer.Id, "USD");

            _userService.Setup(s => s.UserId).Returns(customer.Id);
            _productRepository.Setup(x => x.GetById(command.ProductId)).ReturnsAsync(productList.Products.First());
            _shoppingCartRepository.Setup(x => x.GetShoppingCartByCustomerId(customer.Id)).ReturnsAsync(shoppingCart);
            _currencyConverter.Setup(x => x.GetConvertedPrice(productList.Products.First().Price.Amount,
                                                                          productList.Products.First().Price.Currency,
                                                                          shoppingCart.TotalPrice.Currency))
                                                              .ReturnsAsync(productList.Products.First().Price.Amount);


            shoppingCart.AddProductToShoppingCart(productList.Products.First(),
                                                  command.Quantity,
                                                  productList.Products.First().Price.Amount);

            var result = await _sut.Handle(command, CancellationToken.None);

            _unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
            Assert.IsType<Guid>(result);
        }

        [Fact]
        public async void AddProductToRepository_ThrowsNotFoundException_IfProductsDoesntExist()
        {
            var customer = CustomerFactory.GetCustomer();

            var command = new AddProductToShoppingCartCommand()
            {
                ProductId = Guid.NewGuid(),
                Quantity = 1
            };

            var shoppingCart = ShoppingCart.CreateShoppingCart(customer.Id, "USD");

            _userService.Setup(s => s.UserId).Returns(customer.Id);
            _productRepository.Setup(x => x.GetById(command.ProductId)).ThrowsAsync(new NotFoundException("Product not found."));
            _shoppingCartRepository.Setup(x => x.GetShoppingCartByCustomerId(customer.Id)).ReturnsAsync(shoppingCart);

            var result = await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
            Assert.IsType<NotFoundException>(result);
            Assert.Equal("Product not found.", result.Message);
        }        
    }
}
