using Domain.Shared.ValueObjects;
using Domain.Shop.Entities.Products.ValueObjects;
using Domain.Shop.ValueObjects;

namespace Domain.Shop.Entities.Products.Factories
{
    public interface IProductFactory
    {
        Product Create(ProductId id,
                              ProductName productName,
                              ProductDescription productDescription,
                              ProductPrice productPrice,
                              Unit unit,
                              ShopId shopId);
    }
}