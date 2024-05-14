using actividades.Contracts.usuarios;
using Microsoft.AspNetCore.Mvc;

namespace actividades.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : Controller
    {
        [HttpPost]
        public ActionResult CreateUser(CreateUserRequest request)
        {
            return Ok(request);
        }

        [HttpGet]
        public ActionResult GetAllUsers()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult GetUserById(int id)
        {
            return Ok(id);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, UpdateUserRequest request)
        {
            return Ok(request);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            return Ok(id);
        }
    }
}
