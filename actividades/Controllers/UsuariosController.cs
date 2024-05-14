using actividades.Contracts.usuarios;
using actividades.Services.usuarios;
using Microsoft.AspNetCore.Mvc;

namespace actividades.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : Controller
    {

        private readonly IUsuariosService usuariosService;

        public UsuariosController(IUsuariosService usuariosService)
        {
            this.usuariosService = usuariosService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(CreateUserRequest request)
        {
            var result = await usuariosService.Create(request);

            if (result.validationErrors != null && result.validationErrors.Any())
                return UnprocessableEntity(result.validationErrors);

            return Ok(result.newEntityId);
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
