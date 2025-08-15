using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Unimar.Console.Services;

namespace Unimar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _servico;

        public AuthController(AuthService servico)
        {
            this._servico = servico;
        }


        [AllowAnonymous]
        [HttpPost("token")]
        public IActionResult GenerateToken()
        {
            var token = _servico.RetornaTokenValido();

            return Ok(token);
        }
    }
}
