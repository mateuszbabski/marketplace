using Application.Common.Interfaces;
using Domain.Shops.Entities.Products.Repositories;
using Domain.Shops.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.ChangeProductAvailability
{
    public class ChangeProductAvailabilityCommandHandler : IRequestHandler<ChangeProductAvailabilityCommand, Unit>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICurrentUserService _userService;
        private readonly IShopRepository _shopRepository;

        public ChangeProductAvailabilityCommandHandler(IProductRepository productRepository, ICurrentUserService userService, IShopRepository shopRepository)
        {
            _productRepository = productRepository;
            _userService = userService;
            _shopRepository = shopRepository;
        }


        public async Task<Unit> Handle(ChangeProductAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var shopId = _userService.UserId;
            var shop = await _shopRepository.GetShopById(shopId);
            var product = await _productRepository.GetById(request.Id);

            if (product == null || product.ShopId.Value != shopId)
            {
                throw new Exception("Product not found");
            }

            shop.ChangeProductAvailability(request.Id);

            await _productRepository.Update(product);         
            
            return Unit.Value;
        }
    }
}
