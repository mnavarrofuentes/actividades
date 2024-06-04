using actividades.Contracts.usuarios;
using actividades.Models;

namespace actividades.Services.usuarios
{
    public class UsuariosService : IUsuariosService
    {

        private readonly control_tareasContext dbContext;


        public UsuariosService(control_tareasContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<(List<string>? validationErrors, int? newEntityId)> Create(CreateUserRequest request)
        {

            var data = dbContext.Usuarios.Add(new Usuario
            {
                Nombre = request.Nombre,
                Correo = request.Correo,
                Contrasena = request.Contrasena
            });

            await dbContext.SaveChangesAsync();

            return (null, data.Entity.Id);

        }
    }
}
