using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Shops.Entities.ShopOrders.Repositories;
using MediatR;

namespace Application.Features.ShopOrders.GetShopOrders
{
    public class GetShopOrdersQueryHandler : IRequestHandler<GetShopOrdersQuery, IEnumerable<ShopOrderDto>>
    {
        private readonly ICurrentUserService _userService;
        private readonly IShopOrderRepository _shopOrderRepository;

        public GetShopOrdersQueryHandler(ICurrentUserService userService, IShopOrderRepository shopOrderRepository)
        {
            _userService = userService;
            _shopOrderRepository = shopOrderRepository;
        }
        public async Task<IEnumerable<ShopOrderDto>> Handle(GetShopOrdersQuery request, CancellationToken cancellationToken)
        {
            var userId = _userService.UserId;
            
            var shopOrders = await _shopOrderRepository.GetAllShopOrdersForShop(userId) 
                ?? throw new NotFoundException("There is no shopOrders available for current user");

            var shopOrderListDto = ShopOrderDto.CreateShopOrderDtoFromObject(shopOrders);

            return shopOrderListDto;
        }
    }
}
