using Domain.Shops;
using Domain.Shops.ValueObjects;

namespace Domain.Shops.Repositories
{
    public interface IShopRepository
    {
        Task<Shop> Add(Shop shop);
        Task Update(Shop shop);
        Task<Shop> GetShopByEmail(string email);
        Task<Shop> GetShopById(ShopId id);
    }
}