using Domain.Shop;
using Domain.Shop.ValueObjects;

namespace Domain.Shop.Repositories
{
    public interface IShopRepository
    {
        Task<Shop> Add(Shop shop);
        Task<Shop> GetShopByEmail(string email);
        Task<Shop> GetShopById(ShopId id);
    }
}