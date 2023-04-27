using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ShopOrders.GetShopOrderById
{
    public class GetShopOrderByIdQuery : IRequest<ShopOrderDetailsDto>
    {
        public Guid Id { get; set; }
    }
}
