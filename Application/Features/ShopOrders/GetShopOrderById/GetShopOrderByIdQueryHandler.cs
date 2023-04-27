using Application.Common.Interfaces;
using Domain.Shops.Entities.ShopOrders.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ShopOrders.GetShopOrderById
{
    public class GetShopOrderByIdQueryHandler : IRequestHandler<GetShopOrderByIdQuery, ShopOrderDetailsDto>
    {
        private readonly ICurrentUserService _userService;
        private readonly IShopOrderRepository _shopOrderRepository;

        public GetShopOrderByIdQueryHandler(ICurrentUserService userService, IShopOrderRepository shopOrderRepository)
        {
            _userService = userService;
            _shopOrderRepository = shopOrderRepository;
        }
        public async Task<ShopOrderDetailsDto> Handle(GetShopOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _userService.UserId;

            var shopOrder = await _shopOrderRepository.GetShopOrderById(request.Id, userId) ?? throw new Exception("There is no ShopOrder with requested Id");

            var shopOrderDto = ShopOrderDetailsDto.CreateShopOrderDtoFromObject(shopOrder);

            return shopOrderDto;
        }
    }
}
