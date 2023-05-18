using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Shops;
using Domain.Shops.Entities.Products.Repositories;
using Domain.Shops.Entities.Products.ValueObjects;
using Domain.Shops.Repositories;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.ChangeProductAvailability
{
    public class ChangeProductAvailabilityCommandHandler : IRequestHandler<ChangeProductAvailabilityCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICurrentUserService _userService;
        private readonly IShopRepository _shopRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeProductAvailabilityCommandHandler(IProductRepository productRepository,
                                                       ICurrentUserService userService,
                                                       IShopRepository shopRepository,
                                                       IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _userService = userService;
            _shopRepository = shopRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ChangeProductAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var shopId = _userService.UserId;
            var shop = await _shopRepository.GetShopById(shopId);
            var product = await _productRepository.GetById(request.Id);

            if (product == null || product.ShopId.Value != shopId)
            {
                throw new NotFoundException("Product not found");
            }

            shop.ChangeProductAvailability(request.Id);

            await _unitOfWork.CommitAsync();
        }
    }
}
