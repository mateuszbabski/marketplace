using Application.Common.Interfaces;
using Application.Common.Responses;
using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.Products;
using Domain.Shops.Entities.Products.Repositories;
using Domain.Shops.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.UpdateProductPrice
{
    public class UpdateProductPriceCommandHandler : IRequestHandler<UpdateProductPriceCommand, Unit>
    {
        private readonly ICurrentUserService _userService;
        private readonly IProductRepository _productRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductPriceCommandHandler(ICurrentUserService userService,
                                                IProductRepository productRepository,
                                                IShopRepository shopRepository,
                                                IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _productRepository = productRepository;
            _shopRepository = shopRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken)
        {
            var shopId = _userService.UserId;
            var shop = await _shopRepository.GetShopById(shopId);
            var product = await _productRepository.GetById(request.Id);

            if (product == null || product.ShopId.Value != shopId)
            {
                throw new Exception("Product not found");
            }

            shop.ChangeProductPrice(request.Id,
                                    request.Amount,
                                    request.Currency);

            await _unitOfWork.CommitAsync();

            return Unit.Value;
        }
    }
}
