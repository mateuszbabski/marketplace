using Domain.Customers.Entities.ShoppingCarts.ValueObjects;
using Domain.Customers.Entities.ShoppingCarts;
using Domain.Customers.ValueObjects;
using Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ShoppingCarts
{
    public class ShoppingCartDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
        public MoneyValue TotalPrice { get; set; }
    }
}
