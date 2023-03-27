﻿using Application.Features.Shops;
using Application.Features.Shops.GetShopDetails;
using Application.Features.Shops.UpdateShopDetails;
using Domain.Shop;
using Domain.Shop.Entities.Products;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("{id}", Name = "Get-detailed-shop")]
        public async Task<ActionResult<ShopDto>> GetShopAsync(Guid id)
        {
            var shop = await _mediator.Send(new GetShopDetailsQuery()
            {
                Id = id
            });

            return Ok(shop);
        }        
    }
}
