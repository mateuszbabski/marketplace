using Domain.Shop.Entities.Products.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(request.Id) ?? throw new Exception("Product not found");

            var productVM = new ProductDto()
            {
                Id = product.Id,
                ProductName = product.ProductName, 
                ProductDescription = product.ProductDescription,
                Unit = product.Unit,
                IsAvailable = product.IsAvailable,
                ShopId = product.ShopId,
                Price = product.Price                
            };

            return productVM;
        }
    }
}
