using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Features.ShoppingCarts.RemoveProductFromShoppingCart;
using Domain.Customers.Entities.ShoppingCarts.Repositories;
using Moq;
using UnitTest.Domain.Customers;

namespace UnitTest.Application.ShoppingCarts
{
    public class RemoveProductFromShoppingCartTest
    {
        private readonly RemoveProductFromShoppingCartCommandHandler _sut;
        private readonly Mock<ICurrentUserService> _userService = new();
        private readonly Mock<IShoppingCartRepository> _shoppingCartRepository = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        public RemoveProductFromShoppingCartTest()
        {
            _sut = new RemoveProductFromShoppingCartCommandHandler(_userService.Object,
                                                                   _shoppingCartRepository.Object,
                                                                   _unitOfWork.Object);
        }

        [Fact]
        public async void RemoveProductFromCart_RemovesCart_WhenLastProductIsRemoved()
        {
            var customer = CustomerFactory.GetCustomer();
            var cart = new ShoppingCartMock(customer);

            var command = new RemoveProductFromShoppingCartCommand()
            {
                Id = cart.ShoppingCart.Items[0].Id
            };
            var command2 = new RemoveProductFromShoppingCartCommand()
            {
                Id = cart.ShoppingCart.Items[1].Id
            };

            _userService.Setup(x => x.UserId).Returns(customer.Id);
            _shoppingCartRepository.Setup(x => x.GetShoppingCartByCustomerId(customer.Id)).ReturnsAsync(cart.ShoppingCart);

            await _sut.Handle(command, CancellationToken.None);
            await _sut.Handle(command2, CancellationToken.None);

            _shoppingCartRepository.Setup(x => x.DeleteCart(cart.ShoppingCart)).Equals(true);

            _unitOfWork.Verify(x => x.CommitAsync(), Times.Exactly(2));
            _shoppingCartRepository.Setup(x => x.GetShoppingCartByCustomerId(customer.Id))
                .ThrowsAsync(new NotFoundException("Cart not found."));
        }

        [Fact]
        public async void RemoveProductFromCart_RemovesProduct_IfExists()
        {
            var customer = CustomerFactory.GetCustomer();
            var cart = new ShoppingCartMock(customer);

            var command = new RemoveProductFromShoppingCartCommand()
            {
                Id = cart.ShoppingCart.Items[0].Id
            };

            _userService.Setup(x => x.UserId).Returns(customer.Id);
            _shoppingCartRepository.Setup(x => x.GetShoppingCartByCustomerId(customer.Id)).ReturnsAsync(cart.ShoppingCart);

            await _sut.Handle(command, CancellationToken.None);
            _unitOfWork.Verify(x => x.CommitAsync(), Times.Once);  
            Assert.Single(cart.ShoppingCart.Items);
        }

        [Fact]
        public async void RemoveProductFromCart_ThrowsException_IfCartDoesntExist()
        {
            var customer = CustomerFactory.GetCustomer();

            var command = new RemoveProductFromShoppingCartCommand()
            {
                Id = Guid.NewGuid()
            };

            _userService.Setup(s => s.UserId).Returns(customer.Id);
            _shoppingCartRepository.Setup(x => x.GetShoppingCartByCustomerId(customer.Id)).ThrowsAsync(new NotFoundException("Cart not found."));

            var result = await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, CancellationToken.None));

            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
            Assert.Equal("Cart not found.", result.Message);
        }
    }
}
