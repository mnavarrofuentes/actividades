using actividades.Contracts.auth;
using Microsoft.AspNetCore.Mvc;

namespace actividades.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        [HttpPost("login")]
        public ActionResult Login(LoginRequest request)
        {
            var token = GenerateToken();
            return Ok(new { token });
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
