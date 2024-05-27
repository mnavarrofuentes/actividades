using actividades.Contracts.auth;
using actividades.Services.auth;
using actividades.Services.usuarios;
using Microsoft.AspNetCore.Mvc;

namespace actividades.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {

        private readonly IAuthService authService;


        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }


        [HttpPost("login")]
        public async Task<ActionResult>  Login(LoginRequest request)
        {
            var (response, validationErrors) = await authService.Login(request);

            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(new { errors = validationErrors });
            }
        }

        [HttpPost("logout")]
        public ActionResult Logout()
        {
            return Ok();
        }

        private string GenerateToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
