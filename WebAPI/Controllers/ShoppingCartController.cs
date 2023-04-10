using Application.Features.ShoppingCarts;
using Application.Features.ShoppingCarts.AddProductToShoppingCart;
using Application.Features.ShoppingCarts.DeleteShoppingCart;
using Application.Features.ShoppingCarts.GetShoppingCartByCustomerId;
using Application.Features.ShoppingCarts.RemoveProductFromShoppingCart;
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
        public async Task<ActionResult<Guid>> DeleteShoppingCart()
        {
            await _mediator.Send(new DeleteShoppingCartCommand());

            return NoContent();
        }

        [Authorize(Roles = "customer")]
        [HttpDelete("RemoveProductFromCart")]
        public async Task<ActionResult<Unit>> RemoveProductFromShoppingCart(Guid id)
        {
            await _mediator.Send(new RemoveProductFromShoppingCartCommand()
            {
                Id = id
            });

            return NoContent();
        }

        [Authorize(Roles = "customer")]
        [HttpGet("GetShoppingCartByCustomerId")]
        public async Task<ActionResult<ShoppingCartDto>> GetShoppingCartForCustomer()
        {
            var shoppingCart = await _mediator.Send(new GetShoppingCartByCustomerIdCommand());

            return Ok(shoppingCart);
        }

    }
}
