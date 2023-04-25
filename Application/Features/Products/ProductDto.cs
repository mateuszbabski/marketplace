using Domain.Shared.ValueObjects;
using Domain.Shops;
using Domain.Shops.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products
{
    public record ProductDto
    {
        public Guid Id { get; init; }
        public string ProductName { get; init; }
        public string ProductDescription { get; init; }
        public MoneyValue Price { get; init; }
        public string Unit { get; init; }
        public Guid ShopId { get; init; }
        public bool IsAvailable { get; init; }

        public static ProductDto CreateProductDtoFromObject(Product product)
        {
            return new ProductDto()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                Price = product.Price,
                Unit = product.Unit,
                ShopId = product.ShopId,
                IsAvailable = product.IsAvailable,
            };
        }
    }
}
