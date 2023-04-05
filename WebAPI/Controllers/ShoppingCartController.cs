using Application.Features.Products;
using Application.Features.Products.AddProduct;
using Application.Features.Products.GetProductById;
using Application.Features.ShoppingCarts.AddProductToShoppingCart;
using Application.Features.ShoppingCarts.DeleteShoppingCart;
using Domain.Shared.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShoppingCartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "customer")]
        [HttpPost("AddProductToShoppingCart")]
        public async Task<ActionResult<Guid>> AddProductToShoppingCart([FromBody] AddProductToShoppingCartCommand command)
        {
            return await _mediator.Send(command);
        }

        [Authorize(Roles = "customer")]
        [HttpDelete("DeleteShoppingCart")]
        public async Task<ActionResult<Guid>> DeleteShoppingCart(Guid id)
        {
            await _mediator.Send(new DeleteShoppingCartCommand()
            {
                ShoppingCartId = id
            });

            return NoContent();
        }

        //[HttpGet("{id}", Name = "GetShoppingCartById")]
        //public async Task<ActionResult<ShoppingCartDto>> GetShoppingCartById(Guid id)
        //{
        //    var shoppingCart = await _mediator.Send(new GetShoppingCartByIdCommand()
        //    {
        //        Id = id
        //    });

        //    return Ok(shoppingCart);
        //}

    }
}
