using Application.Authentication;
using Application.Authentication.Services;
using Application.Features.Customers.GetCustomerDetails;
using Application.Features.Entrepreneurs.GetEntrepreneurDetails;
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

        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] LoginRequest request)
        {
            return Ok(await _authenticationService.Login(request));
        }

        [HttpPost("register-customer")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] RegisterCustomerRequest request)
        {
            return Ok(await _authenticationService.RegisterCustomer(request));
        }

        [HttpPost("register-entrepreneur")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] RegisterEntrepreneurRequest request)
        {
            return Ok(await _authenticationService.RegisterEntrepreneur(request));
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

        [HttpGet("Get-detailed-entrepreneur")]
        public async Task<ActionResult<EntrepreneurDto>> GetEntrepreneurAsync(Guid id)
        {
            var entrepreneur = await _mediator.Send(new GetEntrepreneurDetailsQuery()
            {
                Id = id
            });

            return Ok(entrepreneur);
        }
    }
}
