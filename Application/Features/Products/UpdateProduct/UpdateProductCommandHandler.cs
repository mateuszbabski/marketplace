using Application.Common.Interfaces;
using Domain.Shared.ValueObjects;
using Domain.Shop.Entities.Products;
using Domain.Shop.Entities.Products.Repositories;
using Domain.Shop.Entities.Products.ValueObjects;
using Domain.Shop.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICurrentUserService _userService;
        private readonly IShopRepository _shopRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository, ICurrentUserService userService, IShopRepository shopRepository)
        {
            _productRepository = productRepository;
            _userService = userService;
            _shopRepository = shopRepository;
        }
        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var shopId = _userService.UserId;
            var shop = await _shopRepository.GetShopById(shopId);
            var product = await _productRepository.GetById(request.Id);

            if (product == null || product.ShopId.Value != shopId)
            {
                throw new Exception("Product not found");
            }
            
            shop.UpdateProductDetails(request.Id,
                                      request.ProductName,
                                      request.ProductDescription,
                                      request.Unit);

            await _productRepository.Update(product);

            return Unit.Value;
        }
    }
}
