using Domain.Shared.ValueObjects;
using Domain.Shop.Entities.Products.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.Features.Products.AddProduct
{
    public class AddProductCommand : IRequest<Guid>
    {
        public ProductName ProductName { get; set; }
        public ProductDescription ProductDescription { get; set;}
        //public decimal Amount { get; set;}
        //public string Currency { get; set; }
        public MoneyValue Price { get; set;}
        public ProductUnit Unit { get; set;}
    }
}
