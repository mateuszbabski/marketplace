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
        public Guid Id { get; private set; }
        public string ProductName { get; private set; }
        public string ProductDescription { get; private set; }
        public MoneyValue Price { get; private set; }
        public string Unit { get; private set; }
        public Guid ShopId { get; private set; }
        public bool IsAvailable { get; private set; }

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
