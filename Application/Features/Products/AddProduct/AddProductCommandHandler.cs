using Application.Common.Interfaces;
using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.Products.Repositories;
using Domain.Shops.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Features.Products.AddProduct
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;
        private readonly IShopRepository _shopRepository;
        private readonly ICurrentUserService _userService;

        public AddProductCommandHandler(IProductRepository productRepository, IShopRepository shopRepository,
                                        ICurrentUserService userService)
        {
            _productRepository = productRepository;
            _shopRepository = shopRepository;
            _userService = userService;
        }


        public async Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var shopId = _userService.UserId;
            var shop = await _shopRepository.GetShopById(shopId);

            var validator = new AddProductCommandValidator();
            validator.ValidateAndThrow(request);

            var price = MoneyValue.Of(request.Amount, request.Currency);
            
            var product = shop.AddProduct(request.ProductName,
                                          request.ProductDescription,
                                          price,
                                          request.Unit,
                                          shopId);

            await _productRepository.Add(product);
            
            return product.Id;
        }
    }
}
