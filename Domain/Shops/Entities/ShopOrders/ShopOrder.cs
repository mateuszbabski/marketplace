using Domain.Customers.ValueObjects;
using Domain.Customers;
using Domain.Shared.ValueObjects;
using System.Text.Json.Serialization;
using Domain.Shops.Entities.ShopOrders.ValueObjects;
using Domain.Shops.ValueObjects;

namespace Domain.Shops.Entities.ShopOrders
{
    public class ShopOrder
    {
        public ShopOrderId Id { get; private set; }
        public ShopOrderStatus ShopOrderStatus { get; private set; } = ShopOrderStatus.InProgress;
        public CustomerId CustomerId { get; private set; }
        public ShopId ShopId { get; private set; }
        public MoneyValue TotalPrice { get; private set; }
        public Address ShippingAddress { get; private set; }
        public List<ShopOrderItem> ShopOrderItems { get; private set; }
        public DateTime PlacedOn { get; private set; } = DateTime.UtcNow;
        public DateTime? StatusChanged { get; private set; } = null;

        [JsonIgnore]
        public virtual Shop Shop { get; private set; }

        //private ShopOrder() { }
        private ShopOrder() 
        {
            Id = Guid.NewGuid();
            ShopOrderItems = new List<ShopOrderItem>();
        }
        public static ShopOrder CreateShopOrder()
        {
            return new ShopOrder();
        }
    }
}
