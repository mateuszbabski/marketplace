using Application.Features.Orders;
using Application.Features.ShopOrders;
using Domain.Customers.Entities.Orders;
using Domain.Customers.Entities.Orders.ValueObjects;
using Domain.Customers.ValueObjects;
using Domain.Invoices;
using Domain.Invoices.ValueObjects;
using Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Invoices
{
    public record InvoiceDetailsDto
    {
        public Guid Id { get; init; }
        public Guid CustomerId { get; init; }
        public Guid OrderId { get; init; }
        public MoneyValue TotalPrice { get; init; }
        public DateTime CreatedOn { get; init; }
        public string InvoiceStatus { get; init; }
        public DateTime? DateOfPayment { get; init; }
        public static InvoiceDetailsDto CreateInvoiceDetailsDtoFromObject(Invoice invoice)
        {
            return new InvoiceDetailsDto()
            {
                Id = invoice.Id,
                CustomerId = invoice.CustomerId,
                OrderId = invoice.OrderId,
                TotalPrice = invoice.TotalPrice,
                CreatedOn = invoice.CreatedOn,
                InvoiceStatus = invoice.InvoiceStatus.ToString(),
                DateOfPayment = invoice.DateOfPayment
            };
        }
    }
}
