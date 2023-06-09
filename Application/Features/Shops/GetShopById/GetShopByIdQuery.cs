﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shops.GetShopById
{
    public class GetShopByIdQuery : IRequest<ShopDetailsDto>
    {
        public Guid Id { get; set; }
    }
}
