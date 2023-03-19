using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Shops.GetShopDetails
{
    public class GetShopDetailsQuery : IRequest<ShopDto>
    {
        public Guid Id { get; set; }
    }
}
