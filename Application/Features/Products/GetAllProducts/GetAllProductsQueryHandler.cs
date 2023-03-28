using Domain.Shops.Entities.Products;
using Domain.Shops.Entities.Products.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllProducts() ?? throw new Exception("Products not found");

            var productList = new List<ProductDto>();

            foreach (var product in products)
            {
                var productDto = new ProductDto()
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    Unit = product.Unit,
                    IsAvailable = product.IsAvailable,
                    ShopId = product.ShopId,
                    Price = product.Price
                };

                productList.Add(productDto);                
            }            
            
            return productList;
        }
    }
}
