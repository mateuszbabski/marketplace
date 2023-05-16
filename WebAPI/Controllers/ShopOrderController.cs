using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Features.ShopOrders.GetShopOrders;
using Application.Features.ShopOrders;
using Application.Features.ShopOrders.GetShopOrderById;
using Application.Features.ShopOrders.GetShopOrdersByOrderId;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopOrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShopOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "shop")]
        [HttpGet("GetShopOrder")]
        public async Task<ActionResult<ShopOrderDetailsDto>> GetShopOrderByIdForShop(Guid id)
        {
            var order = await _mediator.Send(new GetShopOrderByIdQuery()
            {
                Id = id
            });

            return Ok(order);
        }

        [Authorize(Roles = "shop")]
        [HttpGet("GetAllShopOrders")]
        public async Task<ActionResult<IEnumerable<ShopOrderDto>>> GetAllShopOrdersForShop()
        {
            var orders = await _mediator.Send(new GetShopOrdersQuery());

            return Ok(orders);
        }

        [Authorize(Roles = "customer")]
        [HttpGet("GetAllShopOrdersForCustomerOrder")]
        public async Task<ActionResult<IEnumerable<ShopOrderDto>>> GetAllShopOrdersForCustomersOrder(Guid orderId)
        {
            var orders = await _mediator.Send(new GetShopOrderByOrderIdQuery()
            {
                OrderId = orderId
            });

            return Ok(orders);
        }
    }
}
