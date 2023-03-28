using Domain.Shared.ValueObjects;
using Domain.Shops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public MoneyValue Price { get; set; }
        public string Unit { get; set; }
        public Guid ShopId { get; set; }
        public bool IsAvailable { get; set; }
    }
}
