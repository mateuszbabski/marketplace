﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ShopInvoices.GetShopInvoiceById
{
    public class GetShopInvoiceByIdQuery : IRequest<ShopInvoiceDetailsDto>
    {
        public Guid Id { get; set; }
    }
}
