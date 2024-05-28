using actividades.Contracts.comentarios;
using actividades.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace actividades.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : Controller
    {

        private readonly control_tareasContext _context;

        public ComentarioController(control_tareasContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> AgregarComentario([FromBody] ComentarioRequest comentarioDto)
        {
            try
            {
                // Verificar si el comentarioDto es nulo
                if (comentarioDto == null)
                {
                    return BadRequest("El cuerpo de la solicitud no puede estar vacío");
                }

                // Obtener la tarea correspondiente al ID proporcionado
                var tarea = await _context.Tareas.FindAsync(comentarioDto.TareaId);

                // Verificar si la tarea existe
                if (tarea == null)
                {
                    return NotFound($"No se encontró la tarea con el ID {comentarioDto.TareaId}");
                }

                // Obtener la fecha actual del sistema
                DateTime fechaActual = DateTime.Now;

                // Crear el comentario
                Comentario nuevoComentario = new Comentario
                {
                    Comentario1 = comentarioDto.Contenido,
                    TareaId = comentarioDto.TareaId,
                    UsuarioId = comentarioDto.UsuarioId,
                    Fecha = fechaActual
                };

                // Agregar el comentario a la tarea
                tarea.Comentarios.Add(nuevoComentario);

                // Guardar cambios en la base de datos
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al agregar comentario: {ex.Message}");
            }
        }
    }
}
