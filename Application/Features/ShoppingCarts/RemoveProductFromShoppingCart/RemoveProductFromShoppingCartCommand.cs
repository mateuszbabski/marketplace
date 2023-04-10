using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ShoppingCarts.RemoveProductFromShoppingCart
{
    public class RemoveProductFromShoppingCartCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
