using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ShopOrders.GetShopOrdersByOrderId
{
    public class GetShopOrderByOrderIdQuery : IRequest<IEnumerable<ShopOrderDto>>
    {
        public Guid OrderId { get; set; }
    }
}
