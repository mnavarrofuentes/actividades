using actividades.Contracts.comentarios;
using actividades.Contracts.tareas;
using actividades.Services.Tarea;
using actividades.Services.usuarios;
using Microsoft.AspNetCore.Mvc;

namespace actividades.Controllers
{

    [ApiController]
    [Route("api/tareas")]

    public class TareaController : Controller
    {
        private readonly ITareaService _tareaService;

        public TareaController(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTarea([FromBody] TareaRequest request)
        {
            var tareaResponse = await _tareaService.CreateTareaAsync(request);
            return CreatedAtAction(nameof(GetTareaById), new { id = tareaResponse.Id }, tareaResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTareaById(int id)
        {
            var tareaResponse = await _tareaService.GetTareaByIdAsync(id);
            if (tareaResponse == null)
            {
                return NotFound();
            }
            return Ok(tareaResponse);
        }

        [HttpGet("creador/{creadorId}")]
        public async Task<IActionResult> GetTareasByCreadorId(int creadorId)
        {
            var tareasResponse = await _tareaService.GetTareasByCreadorIdAsync(creadorId);
            return Ok(tareasResponse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTareaAsync(int id, TareaUpdateRequest request)
        {
            var tareaActualizada = await _tareaService.UpdateTareaAsync(id, request);
            if (!tareaActualizada)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("{tareaId}/comentarios")]
        public async Task<ActionResult<IEnumerable<ResponseComentario>>> GetComentarios(int tareaId)
        {
            try
            {
                var comentarios = await _tareaService.GetComentarios(tareaId);
                return Ok(comentarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener comentarios: {ex.Message}");
            }
        }
    }
}
