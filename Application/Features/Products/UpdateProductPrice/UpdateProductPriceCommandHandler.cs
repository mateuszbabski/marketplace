using Application.Common.Interfaces;
using Application.Common.Responses;
using Domain.Shared.ValueObjects;
using Domain.Shop.Entities.Products;
using Domain.Shop.Entities.Products.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.UpdateProductPrice
{
    public class UpdateProductPriceCommandHandler : IRequestHandler<UpdateProductPriceCommand, BaseResponse<Unit>>
    {
        private readonly ICurrentUserService _userService;
        private readonly IProductRepository _productRepository;

        public UpdateProductPriceCommandHandler(ICurrentUserService userService, IProductRepository productRepository)
        {
            _userService = userService;
            _productRepository = productRepository;
        }
        public async Task<BaseResponse<Unit>> Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken)
        {
            var shopId = _userService.UserId;
            var product = await _productRepository.GetById(request.Id);

            if (product == null || product.ShopId.Value != shopId)
            {
                throw new Exception("Product not found");
            }

            var newPrice = MoneyValue.Of(request.Amount, request.Currency);
            product.SetPrice(newPrice);

            await _productRepository.Update(product);

            return new BaseResponse<Unit>()
            {
                Succeeded = true,
                Message = "Price changed successfully"
            };
        }
    }
}
