using Domain.Customers.Entities.ShoppingCarts;
using Domain.Customers.Entities.ShoppingCarts.ValueObjects;
using Domain.Customers.ValueObjects;
using Domain.Shops.Entities.Products.ValueObjects;

namespace Domain.Customers.Entities.ShoppingCarts.Repositories
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> Create(ShoppingCart shoppingCart);
        Task<ShoppingCart> GetShoppingCartByCustomerId(CustomerId customerId);
        Task<ShoppingCart> GetShoppingCartById(ShoppingCartId id);
        Task Update(ShoppingCart shoppingCart);
        Task Delete(ShoppingCart shoppingCart);
        Task RemoveItem(ShoppingCart shoppingCart, ShoppingCartItemId shoppingCartItemId);
    }
}