using actividades.Contracts.usuarios;

namespace actividades.Services.usuarios
{
    public interface IUsuariosService
    {
        Task<(List<string>? validationErrors, int? newEntityId)> Create(CreateUserRequest request);
    }
}
