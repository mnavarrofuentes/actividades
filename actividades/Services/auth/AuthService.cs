using actividades.Contracts.auth;
using actividades.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace actividades.Services.auth
{
    public class AuthService : IAuthService
    {
        private readonly control_tareasContext dbContext;

        private readonly JwtSecurityTokenHandler _jwtHandler = new JwtSecurityTokenHandler();
        private readonly byte[] _jwtSecret = Encoding.UTF8.GetBytes("tu_secreto_secreto_de_suficiente_longitud");


        public AuthService(control_tareasContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<(LoginResponse? response, List<string>? validationErrors)> Login(LoginRequest request)
        {
            // Buscar el usuario en la base de datos
            Usuario? usuario = await dbContext.Usuarios.SingleOrDefaultAsync(u => u.Correo == request.Correo && u.Contrasena == request.Contrasena);

            if (usuario == null)
            {
                return (null, new List<string> { "Usuario no encontrado o credenciales incorrectas." });
            }

            // Crear claims para el token JWT
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim("userId", usuario.Id.ToString()),
                // Puedes agregar más claims según tus necesidades.
            };

            // Crear el token JWT
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7), // Aquí puedes establecer la expiración del token.
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_jwtSecret), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = _jwtHandler.CreateToken(tokenDescriptor);
            string tokenString = _jwtHandler.WriteToken(token);

            var response = new LoginResponse(tokenString, usuario.Nombre);

            return (response, null);
        }
    }
}
