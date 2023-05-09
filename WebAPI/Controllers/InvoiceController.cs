using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Invoices;
using Application.Features.Invoices.GetInvoices;
using Application.Features.Invoices.GetInvoiceById;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly Mediator _mediator;

        public InvoiceController(Mediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "customer")]
        [HttpGet("GetInvoice")]
        public async Task<ActionResult<InvoiceDetailsDto>> GetInvoiceByIdForCustomer(Guid id)
        {
            var invoice = await _mediator.Send(new GetInvoiceByIdQuery()
            {
                Id = id
            });

            return Ok(invoice);
        }

        [Authorize(Roles = "customer")]
        [HttpGet("GetAllCustomerInvoices")]
        public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetAllInvoicesForCustomer()
        {
            var invoices = await _mediator.Send(new GetInvoicesQuery());

            return Ok(invoices);
        }
    }
}
