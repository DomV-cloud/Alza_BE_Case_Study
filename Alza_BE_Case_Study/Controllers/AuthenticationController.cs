using Application.Interfaces.Authentication;
using Contracts.Requests.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alza_BE_Case_Study.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("login", Name = "login")]
        public IActionResult Login(LoginRequest request)
        {
            var response = _authenticationService.Login(
                request.CustomerName,
                request.Password);

            return Ok(response);
        }
    }
}
