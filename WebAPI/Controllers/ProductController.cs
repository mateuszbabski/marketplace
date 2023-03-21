using Application.Features.Products.AddProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddProduct")]
        public async Task<ActionResult<Guid>> AddProduct([FromBody] AddProductCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
