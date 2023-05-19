using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Shops.Entities.ShopOrders.Repositories;
using MediatR;

namespace Application.Features.ShopOrders.GetShopOrdersByOrderId
{
    public class GetShopOrderByOrderIdQueryHandler : IRequestHandler<GetShopOrderByOrderIdQuery, IEnumerable<ShopOrderDto>>
    {
        private readonly ICurrentUserService _userService;
        private readonly IShopOrderRepository _shopOrderRepository;

        public GetShopOrderByOrderIdQueryHandler(ICurrentUserService userService, IShopOrderRepository shopOrderRepository)
        {
            _userService = userService;
            _shopOrderRepository = shopOrderRepository;
        }
        public async Task<IEnumerable<ShopOrderDto>> Handle(GetShopOrderByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _userService.UserId;
            var shopOrderList = await _shopOrderRepository.GetAllShopOrdersByOrderIdForCustomer(request.OrderId, userId)
                ?? throw new NotFoundException("There is no shop orders available for this order and user");

            var shopOrderListDto = ShopOrderDto.CreateShopOrderDtoFromObject(shopOrderList);

            return shopOrderListDto;
        }
    }
}
