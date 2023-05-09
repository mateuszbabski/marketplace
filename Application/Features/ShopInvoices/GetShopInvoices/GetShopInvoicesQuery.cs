﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ShopInvoices.GetShopInvoices
{
    public class GetShopInvoicesQuery : IRequest<IEnumerable<ShopInvoiceDto>>
    {
    }
}
