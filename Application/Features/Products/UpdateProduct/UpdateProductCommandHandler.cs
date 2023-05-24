using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Shops.Entities.Products.Repositories;
using Domain.Shops.Repositories;
using MediatR;

namespace Application.Features.Products.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICurrentUserService _userService;
        private readonly IShopRepository _shopRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IProductRepository productRepository,
                                           ICurrentUserService userService,
                                           IShopRepository shopRepository,
                                           IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _userService = userService;
            _shopRepository = shopRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var shopId = _userService.UserId;
            var shop = await _shopRepository.GetShopById(shopId);
            var product = await _productRepository.GetById(request.Id);

            if (product == null || product.ShopId.Value != shopId)
            {
                throw new NotFoundException("Product not found");
            }            
                        
            shop.ChangeProductDetails(request.Id,
                                      request.ProductName,
                                      request.ProductDescription,
                                      request.Unit);

            await _unitOfWork.CommitAsync();            
        }
    }
}
