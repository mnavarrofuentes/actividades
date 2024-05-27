namespace actividades.Contracts.tareas
{
    public record TareaResponse(
       int Id,
       string Nombre,
       string? Descripcion,
       DateOnly? FechaLimite,
       int? Orden,
       bool? Completada,
       int? CreadorId,
       int? Prioridad
   );
}
