using Application.Features.Shops;
using Application.Features.Shops.GetAllShops;
using Application.Features.Shops.GetShopById;
using Application.Features.Shops.UpdateShopDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShopController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "shop")]
        [HttpPatch("UpdateShopDetails")]
        public async Task<ActionResult> UpdateShopDetails(UpdateShopDetailsCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet("{id}", Name = "GetShopById")]
        public async Task<ActionResult<ShopDetailsDto>> GetShopAsync(Guid id)
        {
            var shop = await _mediator.Send(new GetShopByIdQuery()
            {
                Id = id
            });

            return Ok(shop);
        }

        [HttpGet("GetAllShops")]
        public async Task<ActionResult<IEnumerable<ShopDto>>> GetAllShops()
        {
            var shops = await _mediator.Send(new GetAllShopsQuery());

            return Ok(shops);
        }
    }
}
