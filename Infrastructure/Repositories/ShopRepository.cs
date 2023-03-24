using Domain.Shop;
using Domain.Shop.Repositories;
using Domain.Shop.ValueObjects;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal sealed class ShopRepository : IShopRepository
    {
        private readonly IApplicationDbContext _dbContext;

        public ShopRepository(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Shop> Add(Shop shop)
        {
            await _dbContext.Shops.AddAsync(shop);
            await _dbContext.SaveChangesAsync();

            return shop;
        }

        public async Task<Shop> GetShopByEmail(string email)
        {
            return await _dbContext.Shops.FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<Shop> GetShopById(ShopId id)
        {
            return await _dbContext.Shops.FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
