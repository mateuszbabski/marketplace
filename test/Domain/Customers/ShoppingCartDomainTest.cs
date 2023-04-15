using Domain.Customers.Entities.ShoppingCarts;
using Domain.Customers.Entities.ShoppingCarts.Exceptions;
using UnitTest.Domain.Products;

namespace UnitTest.Domain.Customers
{
    public class ShoppingCartDomainTest
    {
        [Fact]
        public void CreateShoppingCart_ReturnsShoppingCartInstance()
        {
            var customer = CustomerFactory.GetCustomer();
            var shoppingCart = ShoppingCart.CreateShoppingCart(customer.Id);

            Assert.NotNull(shoppingCart);
            Assert.IsType<ShoppingCart>(shoppingCart);          
        }

        [Fact]
        public void AddProductToShoppingCart_ReturnsIfSuccessfully()
        {
            var product = ProductFactory.CreateProduct();
            var shoppingCart = GetShoppingCart();

            shoppingCart.AddProductToShoppingCart(product.Id, 1, product.Price);

            var cartItem = shoppingCart.Items.FirstOrDefault(x => x.ProductId == product.Id);

            Assert.Equal(1, cartItem?.Quantity);
            Assert.Equal(product.Id, cartItem?.ProductId);
            Assert.IsType<ShoppingCartItem>(cartItem);
        }

        [Fact]
        public void AddProductToShoppingCart_ThrowsIfQuantityIsZeroOrLess()
        {
            var product = ProductFactory.CreateProduct();
            var shoppingCart = GetShoppingCart();

            var act = Assert.Throws<InvalidQuantityException>(() => shoppingCart.AddProductToShoppingCart(product.Id, 0, product.Price));

            var cartItem = shoppingCart.Items.FirstOrDefault(x => x.ProductId == product.Id);
            
            Assert.Null(cartItem);
            Assert.IsType<InvalidQuantityException>(act);
        }

        [Fact]
        public void RemoveProductFromShoppingCart_RemovesCartItemFromCart()
        {
            var product = ProductFactory.CreateProduct();
            var shoppingCart = GetShoppingCart();

            shoppingCart.AddProductToShoppingCart(product.Id, 1, product.Price);

            var cartItem = shoppingCart.Items.FirstOrDefault(x => x.ProductId == product.Id);
            
            Assert.Equal(product.Id, cartItem?.ProductId);
            Assert.IsType<ShoppingCartItem>(cartItem);

            shoppingCart.RemoveItemFromCart(cartItem.Id);
            var item = shoppingCart.Items.FirstOrDefault(x => x.Id == cartItem.Id);

            Assert.IsNotType<ShoppingCartItem>(item);
            Assert.Null(item);
        }

        private static ShoppingCart GetShoppingCart()
        {
            var customer = CustomerFactory.GetCustomer();
            return ShoppingCart.CreateShoppingCart(customer.Id);
        }
    }
}
