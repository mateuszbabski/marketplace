using Application.Features.Orders;
using Application.Features.Orders.CancelOrder;
using Application.Features.Orders.GetOrderById;
using Application.Features.Orders.GetOrders;
using Application.Features.Orders.PlaceOrder;
using Application.Features.ShoppingCarts.RemoveProductFromShoppingCart;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "customer")]
        [HttpPost("PlaceOrder")]
        public async Task<ActionResult<Guid>> PlaceOrder([FromBody] PlaceOrderCommand command)
        {
            return await _mediator.Send(command);
        }

        [Authorize(Roles = "customer")]
        [HttpGet("GetOrder")]
        public async Task<ActionResult<OrderDetailsDto>> GetOrderByIdForCustomer(Guid id)
        {
            var order = await _mediator.Send(new GetOrderByIdQuery()
            {
                Id = id
            });

            return Ok(order);
        }

        [Authorize(Roles = "customer")]
        [HttpGet("GetAllCustomerOrders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrdersForCustomer()
        {
            var orders = await _mediator.Send(new GetOrdersQuery());

            return Ok(orders);
        }

        [Authorize(Roles = "customer")]
        [HttpPatch("CancelOrder")]
        public async Task<ActionResult<Unit>> CancelOrder(Guid id)
        {
            await _mediator.Send(new CancelOrderCommand()
            {
                Id = id
            });

            return NoContent();
        }
    }
}
