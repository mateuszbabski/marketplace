using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Features.ShoppingCarts.DeleteShoppingCart;
using Domain.Customers.Entities.ShoppingCarts;
using Domain.Customers.Entities.ShoppingCarts.Repositories;
using Moq;
using UnitTest.Domain.Customers;

namespace UnitTest.Application.ShoppingCarts
{
    public class DeleteShoppingCartTest
    {
        private readonly DeleteShoppingCartCommandHandler _sut;
        private readonly Mock<ICurrentUserService> _userService = new();
        private readonly Mock<IShoppingCartRepository> _shoppingCartRepository = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        public DeleteShoppingCartTest()
        {
            _sut = new DeleteShoppingCartCommandHandler(_userService.Object,
                                                        _shoppingCartRepository.Object,
                                                        _unitOfWork.Object);
        }

        [Fact]
        public async void DeleteShoppingCart_DeletesCart_IfExists()
        {
            var customer = CustomerFactory.GetCustomer();

            var command = new DeleteShoppingCartCommand();

            var shoppingCart = ShoppingCart.CreateShoppingCart(customer.Id, "USD");

            _userService.Setup(s => s.UserId).Returns(customer.Id);
            _shoppingCartRepository.Setup(x => x.GetShoppingCartByCustomerId(customer.Id)).ReturnsAsync(shoppingCart);
            _shoppingCartRepository.Setup(x => x.DeleteCart(shoppingCart)).Equals(true);

            await _sut.Handle(command, CancellationToken.None);

            _unitOfWork.Verify(x => x.CommitAsync(), Times.Once);
        }

        [Fact]
        public async void DeleteShoppingCart_ThrowsNotFoundException_IfDoesntExist()
        {
            var customer = CustomerFactory.GetCustomer();

            var command = new DeleteShoppingCartCommand();

            _userService.Setup(s => s.UserId).Returns(customer.Id);
            _shoppingCartRepository.Setup(x => x.GetShoppingCartByCustomerId(customer.Id)).ThrowsAsync(new NotFoundException("Cart not found."));

            var result = await Assert.ThrowsAsync<NotFoundException>(() =>_sut.Handle(command, CancellationToken.None));

            _unitOfWork.Verify(x => x.CommitAsync(), Times.Never);
            Assert.Equal("Cart not found.", result.Message);
        }
    }
}
