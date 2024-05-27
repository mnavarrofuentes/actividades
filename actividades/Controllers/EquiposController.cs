using actividades.Contracts.equipo;
using actividades.Contracts.tareas;
using actividades.Contracts.usuarios;
using actividades.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace actividades.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquiposController : ControllerBase
    {
        private readonly control_tareasContext _context;

        public EquiposController(control_tareasContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEquipo([FromBody] CreateEquipoRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var equipo = new Equipo
            {
                Nombre = request.Nombre
            };

            _context.Equipos.Add(equipo);
            await _context.SaveChangesAsync();

            var miembroEquipo = new MiembroEquipo
            {
                UsuarioId = request.UsuarioId,
                EquipoId = equipo.Id
            };

            _context.MiembroEquipos.Add(miembroEquipo);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id}/tareas")]
        public async Task<ActionResult<IEnumerable<TareaResponse>>> GetTareasByEquipoId(int id)
        {
            var tareas = await _context.Tareas
                .Where(t => t.AsignacionEquipos.Any(ae => ae.EquipoId == id))
                .ToListAsync();

            if (!tareas.Any())
            {
                return Ok(new List<Tarea>());
            }

            return tareas.Select(t => new TareaResponse(
                t.Id,
                t.Nombre,
                t.Descripcion,
                t.FechaLimite,
                t.Orden,
                t.Completada,
                t.CreadorId,
                t.Prioridad
            )).ToList();
        }

        [HttpGet("usuario/{usuarioId}/equipos")]
        public async Task<ActionResult<IEnumerable<Equipo>>> GetEquiposByUsuarioId(int usuarioId)
        {
            var equipos = await _context.Equipos
                .Where(e => e.MiembroEquipos.Any(me => me.UsuarioId == usuarioId))
                .ToListAsync();

            if (!equipos.Any())
            {
                return NotFound();
            }

            return equipos;
        }

        [HttpGet("{equipoId}/usuariosno")]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsuariosFueraDeEquipo(int equipoId)
        {
            try
            {
                // Obtener todos los usuarios que no están en el equipo específico
                var usuariosFueraDeEquipo = await _context.Usuarios
                    .Where(u => !_context.MiembroEquipos.Any(me => me.UsuarioId == u.Id && me.EquipoId == equipoId))
                    .Select(u => new UserResponse(u.Id, u.Nombre, u.Correo))
                    .ToListAsync();

                // Verificar si no se encontraron usuarios fuera del equipo
                if (!usuariosFueraDeEquipo.Any())
                {
                    return NotFound("No hay usuarios fuera de este equipo.");
                }

                // Devolver la lista de usuarios fuera del equipo
                return Ok(usuariosFueraDeEquipo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        [HttpGet("{equipoId}/usuarios")]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsuariosEnEquipo(int equipoId)
        {
            try
            {
                // Obtener todos los usuarios que están en el equipo específico
                var usuariosEnEquipo = await _context.Usuarios
                    .Where(u => _context.MiembroEquipos.Any(me => me.UsuarioId == u.Id && me.EquipoId == equipoId))
                    .Select(u => new UserResponse(u.Id, u.Nombre, u.Correo))
                    .ToListAsync();

                // Verificar si no se encontraron usuarios en el equipo
                if (!usuariosEnEquipo.Any())
                {
                    return NotFound("No hay usuarios en este equipo.");
                }

                // Devolver la lista de usuarios en el equipo
                return Ok(usuariosEnEquipo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }



        [HttpPost("{equipoId}/asignarmiembro")]
        public async Task<ActionResult> AsignarMiembroAEquipo(int equipoId, [FromBody] AsignarMiembroRequest request)
        {
            try
            {
                // Verificar si el equipo existe
                var equipo = await _context.Equipos.FindAsync(equipoId);
                if (equipo == null)
                {
                    return NotFound("El equipo especificado no existe.");
                }

                // Verificar si el usuario existe
                var usuario = await _context.Usuarios.FindAsync(request.UsuarioId);
                if (usuario == null)
                {
                    return NotFound("El usuario especificado no existe.");
                }

                // Verificar si el usuario ya está asignado al equipo
                var existeAsignacion = await _context.MiembroEquipos
                    .AnyAsync(me => me.UsuarioId == request.UsuarioId && me.EquipoId == equipoId);
                if (existeAsignacion)
                {
                    return BadRequest("El usuario ya está asignado a este equipo.");
                }

                // Crear una nueva asignación de miembro a equipo
                var nuevaAsignacion = new MiembroEquipo
                {
                    UsuarioId = request.UsuarioId,
                    EquipoId = equipoId
                };

                _context.MiembroEquipos.Add(nuevaAsignacion);
                await _context.SaveChangesAsync();

                return Ok("El usuario ha sido asignado al equipo correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }



    }
}
