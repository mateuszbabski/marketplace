using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.GetOrders
{
    public class GetOrdersQuery : IRequest<IEnumerable<OrderDto>>
    {
    }
}
