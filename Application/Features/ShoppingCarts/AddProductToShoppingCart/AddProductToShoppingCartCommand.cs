using Domain.Customers.Entities.ShoppingCarts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ShoppingCarts.AddProductToShoppingCart
{
    public class AddProductToShoppingCartCommand : IRequest<Guid>
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
