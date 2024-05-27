using actividades.Contracts.tareas;
using actividades.Models;
using Microsoft.EntityFrameworkCore;

namespace actividades.Services.Tarea
{
    public class TareaService : ITareaService
    {
        private readonly control_tareasContext _context;

        public TareaService(control_tareasContext context)
        {
            _context = context;
        }

        public async Task<TareaResponse> CreateTareaAsync(TareaRequest request)
        {

            var tarea = new Models.Tarea
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                FechaLimite = request.FechaLimite,
                Orden = request.Orden,
                Completada = request.Completada,
                CreadorId = request.CreadorId,
                Prioridad = request.Prioridad
            };

            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();

            if (request.GrupoId != 0)
            {
                var asignacionEquipo = new AsignacionEquipo
                {
                    TareaId = tarea.Id,
                    EquipoId = request.GrupoId
                };

                _context.AsignacionEquipos.Add(asignacionEquipo);
                await _context.SaveChangesAsync();
            }

            var asignacionUsuario = new AsignacionUsuario
            {
                TareaId = tarea.Id,
                UsuarioId = request.ResponsableId
            };
                
            _context.AsignacionUsuarios.Add(asignacionUsuario);
            await _context.SaveChangesAsync();

            return new TareaResponse(
                tarea.Id,
                tarea.Nombre,
                tarea.Descripcion,
                tarea.FechaLimite,
                tarea.Orden,
                tarea.Completada,
                tarea.CreadorId,
                tarea.Prioridad
            );
        }

        public async Task<TareaResponse?> GetTareaByIdAsync(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
            {
                return null;
            }

            return new TareaResponse(
                tarea.Id,
                tarea.Nombre,
                tarea.Descripcion,
                tarea.FechaLimite,
                tarea.Orden,
                tarea.Completada,
                tarea.CreadorId,
                tarea.Prioridad
            );
        }

        public async Task<List<TareaResponse>> GetTareasByCreadorIdAsync(int creadorId)
        {
            var tareas = await _context.Tareas
                .Where(t => t.CreadorId == creadorId)
                .OrderByDescending(t => t.Orden)  // Ordenar por prioridad descendente
                .ToListAsync();

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


        public async Task<bool> UpdateTareaAsync(int id, TareaUpdateRequest request)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
            {
                return false;
            }
            tarea.Orden = request.Orden;
            tarea.Prioridad = request.Prioridad;
            tarea.Completada = request.Completada;

            await _context.SaveChangesAsync();

            return true;
        }
    }


}
