using actividades.Contracts.comentarios;
using actividades.Contracts.tareas;

namespace actividades.Services.Tarea
{
    public interface ITareaService
    {

        Task<TareaResponse> CreateTareaAsync(TareaRequest request);



        Task<TareaResponse?> GetTareaByIdAsync(int id);


        Task<List<TareaResponse>> GetTareasByCreadorIdAsync(int creadorId);

        Task<bool> UpdateTareaAsync(int id, TareaUpdateRequest request);

        Task<IEnumerable<ResponseComentario>> GetComentarios(int tareaId);

    }
}
