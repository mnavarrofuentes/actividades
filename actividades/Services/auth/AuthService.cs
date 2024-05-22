using actividades.Contracts.auth;

namespace actividades.Services.auth
{
    public class AuthService : IAuthService
    {
        public Task<(List<string>? validationErrors, int? newEntityId)> Login(LoginRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
