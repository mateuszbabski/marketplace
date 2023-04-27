using Domain.Customers;
using Domain.Customers.Entities.Orders;
using Domain.Customers.Entities.ShoppingCarts;
using Domain.Shops.Entities.ShopOrders;
using UnitTest.Domain.Customers;
using UnitTest.Domain.Products;

namespace UnitTest.Domain.Shops
{
    public class ShopOrderDomainTest
    {
        [Fact]
        public void CreateShopOrderFromCustomerOrder_CreatesOrderSuccessfully()
        {
            var customer = CustomerFactory.GetCustomer();
            var cart = CreateCartSample(customer);
            var order = CreateOrderSample(customer, cart);

            var shopOrder = ShopOrder.CreateShopOrder(order, cart.Items);

            Assert.IsType<ShopOrder>(shopOrder);
            Assert.Equal(order.Id, shopOrder.OrderId);
            Assert.Equal(cart.Items.First().ShopId, shopOrder.ShopId);
        }

        private static ShoppingCart CreateCartSample(Customer customer)
        {
            var shoppingCart = ShoppingCart.CreateShoppingCart(customer.Id);
            var product = ProductFactory.CreateProduct();

            shoppingCart.AddProductToShoppingCart(product, 1);

            return shoppingCart;
        }
        private static Order CreateOrderSample(Customer customer, ShoppingCart shoppingCart)
        {          
            var order = customer.PlaceOrder(shoppingCart, customer.Address, DateTime.UtcNow);
            return order;
        }
    }
}
