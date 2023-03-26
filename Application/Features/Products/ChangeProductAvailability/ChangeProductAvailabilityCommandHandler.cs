using Application.Common.Interfaces;
using Domain.Shop.Entities.Products.Repositories;
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

        public ChangeProductAvailabilityCommandHandler(IProductRepository productRepository, ICurrentUserService userService)
        {
            _productRepository = productRepository;
            _userService = userService;
        }
        public async Task<Unit> Handle(ChangeProductAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var shopId = _userService.UserId;
            var product = await _productRepository.GetById(request.Id);

            if (product == null || product.ShopId.Value != shopId)
            {
                throw new Exception("Product not found");
            }

            if (product.IsAvailable)
            {
                product.Remove();
            }
            else
            {
                product.Restore();
            }

            await _productRepository.Update(product);

            return Unit.Value;
        }
    }
}
