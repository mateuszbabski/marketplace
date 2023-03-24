using Application.Authentication;
using Application.Authentication.Services;
using Application.Features.Customers.GetCustomerDetails;
using Application.Features.Shops.GetShopDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMediator _mediator;

        public AuthenticationController(IAuthenticationService authenticationService, IMediator mediator)
        {
            _authenticationService = authenticationService;
            _mediator = mediator;
        }

        [HttpPost("login-customer")]
        public async Task<IActionResult> AuthenticateCustomerAsync([FromBody] LoginRequest request)
        {
            return Ok(await _authenticationService.LoginCustomer(request));
        }

        [HttpPost("login-shop")]
        public async Task<IActionResult> AuthenticateShopAsync([FromBody] LoginRequest request)
        {
            return Ok(await _authenticationService.LoginShop(request));
        }

        [HttpPost("register-customer")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] RegisterCustomerRequest request)
        {
            return Ok(await _authenticationService.RegisterCustomer(request));
        }

        [HttpPost("register-shop")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] RegisterShopRequest request)
        {
            return Ok(await _authenticationService.RegisterShop(request));
        }

        [HttpGet("Get-detailed-customer")]
        public async Task<ActionResult<CustomerDto>> GetCustomerAsync(Guid id)
        {
            var customer = await _mediator.Send(new GetCustomerDetailsQuery()
            {
                Id = id
            });

            return Ok(customer);
        }

        [HttpGet("Get-detailed-shop")]
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
