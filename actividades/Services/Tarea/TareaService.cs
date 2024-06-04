using actividades.Contracts.comentarios;
using actividades.Contracts.tareas;
using actividades.Models;
using Microsoft.AspNetCore.Mvc;
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

            var usuarioAsignado = await _context.Usuarios.FindAsync(request.ResponsableId);
            var correoUsuarioAsignado = usuarioAsignado?.Correo;

            return new TareaResponse(
                tarea.Id,
                tarea.Nombre,
                tarea.Descripcion,
                tarea.FechaLimite,
                tarea.Orden,
                tarea.Completada,
                tarea.CreadorId,
                tarea.Prioridad,
                correoUsuarioAsignado
            );
        }

        public async Task<TareaResponse?> GetTareaByIdAsync(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
            {
                return null;
            }

            var asignacionUsuario = await _context.AsignacionUsuarios.FirstOrDefaultAsync(au => au.TareaId == id);
            var usuarioAsignado = asignacionUsuario != null ? await _context.Usuarios.FindAsync(asignacionUsuario.UsuarioId) : null;
            var correoUsuarioAsignado = usuarioAsignado?.Correo;



            return new TareaResponse(
                tarea.Id,
                tarea.Nombre,
                tarea.Descripcion,
                tarea.FechaLimite,
                tarea.Orden,
                tarea.Completada,
                tarea.CreadorId,
                tarea.Prioridad,
                correoUsuarioAsignado
            );
        }

        public async Task<List<TareaResponse>> GetTareasByCreadorIdAsync(int creadorId)
        {
            var tareas = await _context.Tareas
            .Include(t => t.AsignacionUsuarios)
            .Where(t => t.CreadorId == creadorId)
            .OrderByDescending(t => t.Orden)
            .ToListAsync();

            var tareaResponses = new List<TareaResponse>();

            foreach (var tarea in tareas)
            {
                // Obtener el correo electrónico del usuario asignado
                var usuarioAsignado = tarea.AsignacionUsuarios.FirstOrDefault();
                var correoUsuarioAsignado = usuarioAsignado != null ? await _context.Usuarios.FindAsync(usuarioAsignado.UsuarioId) : null;
                var correo = correoUsuarioAsignado?.Correo;

                tareaResponses.Add(new TareaResponse(
                    tarea.Id,
                    tarea.Nombre,
                    tarea.Descripcion,
                    tarea.FechaLimite,
                    tarea.Orden,
                    tarea.Completada,
                    tarea.CreadorId,
                    tarea.Prioridad,
                    correo
                ));
            }

            return tareaResponses;
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


        public async Task<IEnumerable<ResponseComentario>> GetComentarios(int tareaId)
        {
            var tarea = await _context.Tareas
                .Include(t => t.Comentarios)
                .ThenInclude(c => c.Usuario) // Incluir el Usuario asociado al Comentario
                .FirstOrDefaultAsync(t => t.Id == tareaId);

            if (tarea == null)
            {
                throw new InvalidOperationException($"No se encontró la tarea con ID {tareaId}");
            }

            var comentarios = tarea.Comentarios.Select(c => new ResponseComentario
            {
                Id =c.Id,
                Contenido = c.Comentario1,
                UsuarioEmail = c.Usuario != null ? c.Usuario.Correo : "Usuario desconocido"
            }).ToList();

            return comentarios;
        }
    }


}
