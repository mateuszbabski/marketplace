using Application.Common.Interfaces;
using Domain.Shared.ValueObjects;
using Domain.Shop.Entities.Products;
using Domain.Shop.Entities.Products.Repositories;
using Domain.Shop.Entities.Products.ValueObjects;
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

        public UpdateProductCommandHandler(IProductRepository productRepository, ICurrentUserService userService)
        {
            _productRepository = productRepository;
            _userService = userService;
        }
        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var shopId = _userService.UserId;
            var product = await _productRepository.GetById(request.Id);

            if (product == null || product.ShopId.Value != shopId)
            {
                throw new Exception("Product not found");
            }

            product.SetName(request.ProductName);
            product.SetDescription(request.ProductDescription);
            product.SetUnit(request.Unit);

            await _productRepository.Update(product);

            return Unit.Value;
        }
    }
}
