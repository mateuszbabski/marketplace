using Application.Common.Interfaces;
using Domain.Customers.Entities.Orders.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.GetOrderById
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDetailsDto>
    {
        private readonly ICurrentUserService _userService;
        private readonly IOrderRepository _orderRepository;

        public GetOrderByIdQueryHandler(ICurrentUserService userService, IOrderRepository orderRepository)
        {
            _userService = userService;
            _orderRepository = orderRepository;
        }
        public async Task<OrderDetailsDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var customerId = _userService.UserId;
            var order = await _orderRepository.GetOrderById(request.Id, customerId);

            if (order == null)
            {
                throw new Exception("There is no Order with requested Id");
            }

            var orderDto = OrderDetailsDto.CreateOrderDetailsDtoFromObject(order);
            
            return orderDto;
        }
    }
}
