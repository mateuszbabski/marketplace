using Domain.Customers.Entities.ShoppingCarts.ValueObjects;
using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.Products.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ShoppingCarts
{
    public class ShoppingCartItemDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public MoneyValue Price { get; set; }
    }
}
