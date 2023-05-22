using Domain.Customers;
using Domain.Customers.Entities.Orders;
using Domain.Customers.Entities.ShoppingCarts;
using Domain.Invoices;
using Domain.Shops.Entities.Products;
using Domain.Shops.Entities.ShopOrders;
using MediatR.NotificationPublishers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.Domain.Customers;
using UnitTest.Domain.Products;

namespace UnitTest.Domain.Invoices
{
    public class InvoicesDomainTest
    {        
        [Fact]
        public void CreatingInvoices_SuccessfullWhenOrderIsValid()
        {
            var order = SampleOrder();

            var invoice = Invoice.CreateInvoice(order, DateTime.UtcNow);
            var shopInvoicesList = invoice.ShopInvoices.ToList();

            Assert.IsType<Invoice>(invoice);
            Assert.Equal(order.Id, invoice.OrderId);
            Assert.Equal(order.CustomerId, invoice.CustomerId);

            Assert.IsType<ShopInvoice>(invoice.ShopInvoices.First());
            Assert.IsType<List<ShopInvoice>>(invoice.ShopInvoices.ToList());
            Assert.Equal(2, invoice.ShopInvoices.Count);
            Assert.NotEqual(shopInvoicesList[0].ShopId, shopInvoicesList[1].ShopId);
        }

        [Fact]
        public void CancelInvoice_SuccessfullWhenInvoiceIsNotPaidYet()
        {
            var order = SampleOrder();

            var invoice = Invoice.CreateInvoice(order, DateTime.UtcNow);
            var shopInvoicesList = invoice.ShopInvoices.ToList();

            Assert.IsType<Invoice>(invoice);
            Assert.NotEqual(InvoiceStatus.Paid, invoice.InvoiceStatus);
            Assert.NotEqual(InvoiceStatus.Paid, shopInvoicesList[0].InvoiceStatus);
            Assert.NotEqual(InvoiceStatus.Paid, shopInvoicesList[1].InvoiceStatus);

            invoice.CancelInvoice();

            Assert.Equal(InvoiceStatus.Cancelled, invoice.InvoiceStatus);
            Assert.Equal(InvoiceStatus.Cancelled, shopInvoicesList[0].InvoiceStatus);
            Assert.Equal(InvoiceStatus.Cancelled, shopInvoicesList[1].InvoiceStatus);
        }

        private static Order SampleOrder()
        {
            var customer = CustomerFactory.GetCustomer();
            var shoppingCart = ShoppingCart.CreateShoppingCart(customer.Id, "USD");
            var product = ProductFactory.CreateProduct();
            var product2 = ProductFactory.CreateProduct();

            shoppingCart.AddProductToShoppingCart(product, 1, product.Price.Amount);
            shoppingCart.AddProductToShoppingCart(product2, 1, product2.Price.Amount);

            var order = customer.PlaceOrder(shoppingCart, customer.Address, DateTime.UtcNow);

            return order;
        }        
    }
}
