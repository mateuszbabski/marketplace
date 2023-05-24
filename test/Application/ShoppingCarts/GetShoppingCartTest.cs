using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Features.ShoppingCarts;
using Application.Features.ShoppingCarts.GetShoppingCartByCustomerId;
using Domain.Customers.Entities.ShoppingCarts;
using Domain.Customers.Entities.ShoppingCarts.Repositories;
using Moq;
using UnitTest.Domain.Customers;

namespace UnitTest.Application.ShoppingCarts
{
    public class GetShoppingCartTest
    {
        private readonly GetShoppingCartByCustomerIdCommandHandler _sut;
        private readonly Mock<ICurrentUserService> _userService = new();
        private readonly Mock<IShoppingCartRepository> _shoppingCartRepository = new();
        public GetShoppingCartTest()
        {
            _sut = new GetShoppingCartByCustomerIdCommandHandler(_userService.Object,
                                                                 _shoppingCartRepository.Object);
        }

        [Fact]
        public async void GetShoppingCart_ReturnsCart_IfCustomerHasOne()
        {
            var customer = CustomerFactory.GetCustomer();
            var shoppingCart = ShoppingCart.CreateShoppingCart(customer.Id, "USD");

            _userService.Setup(x => x.UserId).Returns(customer.Id);
            _shoppingCartRepository.Setup(x => x.GetShoppingCartByCustomerId(customer.Id)).ReturnsAsync(shoppingCart);

            var result = await _sut.Handle(new GetShoppingCartByCustomerIdCommand(), CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<ShoppingCartDto>(result);
            Assert.Equal(customer.Id, shoppingCart.CustomerId);
        }

        [Fact]
        public async void GetShoppingCart_ThrowsException_IfCustomerDoesntHaveAny()
        {
            var customer = CustomerFactory.GetCustomer();

            _userService.Setup(x => x.UserId).Returns(customer.Id);
            _shoppingCartRepository.Setup(x => x.GetShoppingCartByCustomerId(customer.Id))
                .ThrowsAsync(new NotFoundException("Cart not found."));

            var result = await Assert.ThrowsAsync<NotFoundException>(() =>_sut.Handle(new GetShoppingCartByCustomerIdCommand(),
                                                                                      CancellationToken.None));

            Assert.IsType<NotFoundException>(result);
            Assert.Equal("Cart not found.", result.Message);
        }
    }
}
