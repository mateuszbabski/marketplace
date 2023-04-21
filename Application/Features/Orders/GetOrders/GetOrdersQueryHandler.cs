using Application.Common.Interfaces;
using Domain.Customers.Entities.Orders.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDto>>
    {
        private readonly ICurrentUserService _userService;
        private readonly IOrderRepository _orderRepository;

        public GetOrdersQueryHandler(ICurrentUserService userService, IOrderRepository orderRepository)
        {
            _userService = userService;
            _orderRepository = orderRepository;
        }
        public async Task<IEnumerable<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var customerId = _userService.UserId;
            var orders = await _orderRepository.GetAllOrdersForCustomer(customerId);

            if (orders == null)
            {
                throw new Exception("There is no orders available for current user");
            }

            var orderList = new List<OrderDto>();

            foreach (var order in orders)
            {
                var orderDto = new OrderDto()
                {
                    Id = order.Id,
                    CustomerId = customerId,
                    OrderStatus = order.OrderStatus.ToString(),
                    TotalPrice = order.TotalPrice,
                    PlacedOn = order.PlacedOn
                };

                orderList.Add(orderDto);
            }

            return orderList.AsEnumerable();
        }
    }
}
