using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.PlaceOrder
{
    public class PlaceOrderCommand : IRequest<Guid>
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
    }
}
