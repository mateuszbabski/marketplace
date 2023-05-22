using Domain.Customers.Entities.ShoppingCarts.Events;
using Domain.Customers.Entities.ShoppingCarts.ValueObjects;
using Domain.Customers.ValueObjects;
using Domain.Shared.Abstractions;
using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.Products;

namespace Domain.Customers.Entities.ShoppingCarts
{
    public class ShoppingCart : Entity
    {
        public ShoppingCartId Id { get; private set; }
        public CustomerId CustomerId { get; private set; }
        public List<ShoppingCartItem> Items { get; private set; }
        public MoneyValue TotalPrice { get; private set; }

        private ShoppingCart() { }

        private ShoppingCart(CustomerId customerId, string currency)
        {
            Id = new ShoppingCartId(Guid.NewGuid());
            CustomerId = customerId;
            Items = new List<ShoppingCartItem>();
            TotalPrice = new MoneyValue(0, currency);
        }

        public static ShoppingCart CreateShoppingCart(CustomerId customerId, string currency)
        {
            var shoppingCart = new ShoppingCart(customerId, currency);
            shoppingCart.AddDomainEvent(new ShoppingCartCreatedDomainEvent(shoppingCart));

            return shoppingCart;
        }

        private MoneyValue CountTotalPrice(List<ShoppingCartItem> items) 
        {
            decimal allProductsPrice = items.Sum(x => x.Price.Amount);

            return new MoneyValue(allProductsPrice, TotalPrice.Currency);
        }

        internal MoneyValue GetPrice()
        {
            return TotalPrice;
        }

        public void ChangeShoppingCartCurrency(decimal conversionRate, string currency)
        {
            foreach (var item in Items)
            {
                item.ChangeCurrency(conversionRate, currency);
            }

            decimal convertedProductsPrice = Items.Sum(x => x.Price.Amount);

            this.TotalPrice = MoneyValue.Of(convertedProductsPrice, currency);

            AddDomainEvent(new ShoppingCartCurrencyChangedDomainEvent(this));
        }

        public void AddProductToShoppingCart(Product product, int quantity, decimal convertedPrice)
        {
            // try to move conversion here
            var shoppingCartItem = Items.FirstOrDefault(x => x.ProductId == product.Id);

            if (shoppingCartItem == null)
            {
                var newShoppingCartItem = ShoppingCartItem.CreateShoppingCartItemFromProduct(product,
                                                                                             this.Id,
                                                                                             quantity,
                                                                                             this.TotalPrice.Currency,
                                                                                             convertedPrice);
                Items.Add(newShoppingCartItem);

                this.AddDomainEvent(new ProductAddedToShoppingCartDomainEvent(this, newShoppingCartItem));
            }
            else
            {
                shoppingCartItem.ChangeCartItemQuantity(quantity, convertedPrice, this.TotalPrice.Currency);
                this.AddDomainEvent(new ProductQuantityChangedDomainEvent(this, shoppingCartItem));
            }
            
            this.TotalPrice = CountTotalPrice(this.Items);
        }
        
        public void RemoveItemFromCart(ShoppingCartItemId shoppingCartItemId)
        {
            var item = Items.FirstOrDefault(x => x.Id == shoppingCartItemId);

            Items.Remove(item);

            this.AddDomainEvent(new ProductRemovedFromShoppingCartDomainEvent(this, item));

            this.TotalPrice = CountTotalPrice(this.Items);
        }
    }
}
