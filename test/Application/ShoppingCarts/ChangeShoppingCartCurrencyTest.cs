using Application.Common.Interfaces;
using Application.Features.ShoppingCarts.ChangeShoppingCartCurrency;
using Domain.Customers.Entities.ShoppingCarts.Repositories;
using Domain.Shops.Entities.Products.Exceptions;
using Moq;
using UnitTest.Domain.Customers;

namespace UnitTest.Application.ShoppingCarts
{
    public class ChangeShoppingCartCurrencyTest
    {
        private readonly ChangeShoppingCartCurrencyCommandHandler _sut;
        private readonly Mock<ICurrentUserService> _userService = new();
        private readonly Mock<IShoppingCartRepository> _shoppingCartRepository = new();
        private readonly Mock<ICurrencyConverter> _currencyConverter = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        public ChangeShoppingCartCurrencyTest()
        {
            _sut = new ChangeShoppingCartCurrencyCommandHandler(_userService.Object,
                                                                _shoppingCartRepository.Object,
                                                                _currencyConverter.Object,
                                                                _unitOfWork.Object);
        }

        [Fact]
        public async void ChangeCartCurrency_Successfull_IfCurrencyIsInSystem()
        {
            var customer = CustomerFactory.GetCustomer();
            var cart = new ShoppingCartMock(customer);

            var command = new ChangeShoppingCartCurrencyCommand()
            {
                Currency = "PLN"
            };

            _userService.Setup(x => x.UserId).Returns(customer.Id);
            _shoppingCartRepository.Setup(x => x.GetShoppingCartByCustomerId(customer.Id)).ReturnsAsync(cart.ShoppingCart);

            _currencyConverter.Setup(x => x.GetConversionRate(cart.ShoppingCart.TotalPrice.Currency, command.Currency))
                .ReturnsAsync(4.18m);

            await _sut.Handle(command, CancellationToken.None);

            Assert.Equal("PLN", cart.ShoppingCart.TotalPrice.Currency);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Once());
        }

        [Fact]
        public async void ChangeCartCurrency_Failed_IfCurrencyIsNotSystem()
        {
            var customer = CustomerFactory.GetCustomer();
            var cart = new ShoppingCartMock(customer);

            var command = new ChangeShoppingCartCurrencyCommand()
            {
                Currency = "XXX"
            };

            _userService.Setup(x => x.UserId).Returns(customer.Id);
            _shoppingCartRepository.Setup(x => x.GetShoppingCartByCustomerId(customer.Id)).ReturnsAsync(cart.ShoppingCart);

            var result = await Assert.ThrowsAsync<InvalidProductPriceException>(() => _sut.Handle(command, CancellationToken.None));
            Assert.Equal("Invalid currency.", result.Message);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
        }
    } 
}
