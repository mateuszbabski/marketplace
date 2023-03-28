using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.Products.ValueObjects;
using Domain.Shops.ValueObjects;

namespace Domain.Shops.Entities.Products.Factories
{
    public interface IProductFactory
    {
        Product Create(ProductId id,
                       ProductName productName,
                       ProductDescription productDescription,
                       MoneyValue price,
                       ProductUnit unit,
                       ShopId shopId);
        
    }
}