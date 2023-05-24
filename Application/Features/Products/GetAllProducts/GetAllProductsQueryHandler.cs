using Application.Common.Exceptions;
using Domain.Shops.Entities.Products.Repositories;
using MediatR;

namespace Application.Features.Products.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllProducts() 
                ?? throw new NotFoundException("Products not found");

            var productList = ProductDto.CreateProductDtoFromObject(products);    
            
            return productList;
        }
    }
}
