using actividades.Contracts.auth;

namespace actividades.Services.auth
{
    public interface IAuthService
    {
        Task<(List<string>? validationErrors, int? newEntityId)> Login(LoginRequest request);
    }
}
