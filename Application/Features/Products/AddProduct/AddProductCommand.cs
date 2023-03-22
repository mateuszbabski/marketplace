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
        public string ProductName { get; set; }
        public string ProductDescription { get; set;}
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "PLN";
        public string Unit { get; set; } = "Piece";
    }
}
