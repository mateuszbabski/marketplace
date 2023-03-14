using Application.Authentication;
using Application.Authentication.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        //[HttpPost("authenticate")]
        //public async Task<IActionResult> AuthenticateAsync([FromBody] LoginRequest request)
        //{
        //    return Ok(await _authenticationService.Login(request));
        //}

        [HttpPost("register-customer")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] RegisterCustomerRequest request)
        {
            return Ok(await _authenticationService.RegisterCustomer(request));
        }
    }
}
