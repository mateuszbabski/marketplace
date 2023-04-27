using Application.Features.Shops;
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

        public static IEnumerable<ProductDto> CreateProductDtoFromObject(IEnumerable<Product> products)
        {
            var productList = new List<ProductDto>();

            foreach (var product in products)
            {
                var productDto = new ProductDto()
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    Price = product.Price,
                    Unit = product.Unit,
                    ShopId = product.ShopId,
                    IsAvailable = product.IsAvailable,
                };

                productList.Add(productDto);
            }
            return productList;
        }
    }
}
