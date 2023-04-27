﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ShopOrders.GetShopOrders
{
    public class GetShopOrdersQuery : IRequest<IEnumerable<ShopOrderDto>>
    {
    }
}
