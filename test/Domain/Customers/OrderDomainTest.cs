using Domain.Customers.Entities.Orders;
using Domain.Customers.Entities.ShoppingCarts;
using Domain.Shared.ValueObjects;
using UnitTest.Domain.Products;

namespace UnitTest.Domain.Customers
{
    public class OrderDomainTest
    {
        [Fact]
        public void CreateOrderFromShoppingCart_SuccessfullyIfCartExists()
        {
            var customer = CustomerFactory.GetCustomer();
            var shoppingCart = GetSampleCart(customer.Id);

            var order = customer.PlaceOrder(shoppingCart, customer.Address, DateTime.UtcNow);

            Assert.IsType<Order>(order);
            Assert.Equal(order.CustomerId, customer.Id);
            Assert.IsType<List<OrderItem>>(order.OrderItems);
        }

        [Fact]
        public void CancelOrder_CancelsOrderIfExists()
        {
            var customer = CustomerFactory.GetCustomer();
            var shoppingCart = GetSampleCart(customer.Id);

            var order = customer.PlaceOrder(shoppingCart, customer.Address, DateTime.UtcNow);

            Assert.Equal(OrderStatus.WaitingForPayment, order.OrderStatus);

            customer.CancelOrder(order.Id);

            Assert.Equal(OrderStatus.Cancelled, order.OrderStatus);
        }

        private static ShoppingCart GetSampleCart(Guid customerId)
        {
            var shoppingCart = ShoppingCart.CreateShoppingCart(customerId);
            var product = ProductFactory.CreateProduct();
            shoppingCart.AddProductToShoppingCart(product.Id, 10, MoneyValue.Of(10, "PLN"));

            return shoppingCart;
        }    
    }
}
