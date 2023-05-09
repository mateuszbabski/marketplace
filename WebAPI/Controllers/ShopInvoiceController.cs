using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Features.ShopInvoices;
using Application.Features.ShopInvoices.GetShopInvoices;
using Application.Features.ShopInvoices.GetShopInvoiceById;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopInvoiceController : ControllerBase
    {
        private readonly Mediator _mediator;

        public ShopInvoiceController(Mediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "shop")]
        [HttpGet("GetShopInvoice")]
        public async Task<ActionResult<ShopInvoiceDetailsDto>> GetInvoiceByIdForShop(Guid id)
        {
            var invoice = await _mediator.Send(new GetShopInvoiceByIdQuery()
            {
                Id = id
            });

            return Ok(invoice);
        }

        [Authorize(Roles = "shop")]
        [HttpGet("GetAllShopInvoices")]
        public async Task<ActionResult<IEnumerable<ShopInvoiceDto>>> GetAllInvoicesForShop()
        {
            var invoices = await _mediator.Send(new GetShopInvoicesQuery());

            return Ok(invoices);
        }
    }
}
