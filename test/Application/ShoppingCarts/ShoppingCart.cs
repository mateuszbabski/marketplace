using Domain.Customers;
using Domain.Customers.Entities.ShoppingCarts;
using UnitTest.Application.Products;

namespace UnitTest.Application.ShoppingCarts
{
    public class ShoppingCartMock
    {
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCart ShoppingCart
        {
            get
            {
                return _shoppingCart;
            }
        }

        public ShoppingCartMock(Customer customer)
        {
            var cart = ShoppingCart.CreateShoppingCart(customer.Id, "USD");

            var products = new ProductList();

            cart.AddProductToShoppingCart(products.Products[0], 1, products.Products[0].Price.Amount);
            cart.AddProductToShoppingCart(products.Products[1], 1, products.Products[1].Price.Amount);

            _shoppingCart = cart;
        }
    }
}
