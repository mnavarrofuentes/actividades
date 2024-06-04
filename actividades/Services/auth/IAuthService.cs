using actividades.Contracts.auth;

namespace actividades.Services.auth
{
    public interface IAuthService
    {
        Task<(LoginResponse? response, List<string>? validationErrors)> Login(LoginRequest request);
    }
}
