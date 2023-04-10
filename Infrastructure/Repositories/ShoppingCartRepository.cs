using Domain.Customers.Entities.ShoppingCarts;
using Domain.Customers.Entities.ShoppingCarts.Repositories;
using Domain.Customers.Entities.ShoppingCarts.ValueObjects;
using Domain.Customers.ValueObjects;
using Domain.Shops.Entities.Products.ValueObjects;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal sealed class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public ShoppingCartRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ShoppingCart> Create(ShoppingCart shoppingCart)
        {
            await _dbContext.ShoppingCarts.AddAsync(shoppingCart);
            await _dbContext.SaveChangesAsync();

            return shoppingCart;
        }

        public async Task Update(ShoppingCart shoppingCart)
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveItem(ShoppingCart shoppingCart, ShoppingCartItemId shoppingCartItemId)
        {
            var product = await _dbContext.ShoppingCartItems.Where(x => x.ShoppingCartId == shoppingCart.Id)
                                                            .FirstOrDefaultAsync(x => x.Id == shoppingCartItemId);

            _dbContext.ShoppingCartItems.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(ShoppingCart shoppingCart)
        {
            _dbContext.ShoppingCarts.Remove(shoppingCart);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ShoppingCart> GetShoppingCartById(ShoppingCartId id)
        {
            return await _dbContext.ShoppingCarts.Include(x => x.Items)
                                                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ShoppingCart> GetShoppingCartByCustomerId(CustomerId customerId)
        {
            return await _dbContext.ShoppingCarts.Include(x => x.Items)
                                                 .FirstOrDefaultAsync(x => x.CustomerId == customerId);
        }
    }
}
