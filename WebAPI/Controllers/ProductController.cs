using Application.Common.Responses;
using Application.Features.Products;
using Application.Features.Products.AddProduct;
using Application.Features.Products.ChangeProductAvailability;
using Application.Features.Products.GetAllProducts;
using Application.Features.Products.GetProductById;
using Application.Features.Products.UpdateProduct;
using Application.Features.Products.UpdateProductPrice;
using Domain.Shared.ValueObjects;
using Domain.Shops.Entities.Products;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "shop")]
        [HttpPost("AddProduct")]
        public async Task<ActionResult<Guid>> AddProduct([FromBody] AddProductCommand command)
        {
            return await _mediator.Send(command);
        }
        
        [Authorize(Roles = "shop")]
        [HttpPatch("{id}/updateprice", Name = "UpdateProductPrice")]
        public async Task<ActionResult> UpdateProductPrice([FromBody] UpdateProductPriceCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [Authorize(Roles = "shop")]
        [HttpPatch("{id}/update", Name = "UpdateProduct")]
        public async Task<ActionResult> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [Authorize(Roles = "shop")]
        [HttpPatch("ChangeProductAvailability")]
        public async Task<ActionResult> ChangeProductAvailability(ChangeProductAvailabilityCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<ActionResult<ProductDto>> GetProductById(Guid id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery()
            {
                Id = id
            });

            return Ok(product);
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<List<ProductDto>>> GetAllProducts()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());

            return Ok(products);
        }
    }
}
