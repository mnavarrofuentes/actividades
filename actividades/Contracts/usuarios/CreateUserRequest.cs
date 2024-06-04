namespace actividades.Contracts.usuarios;

public record CreateUserRequest(
    string Nombre,
    string Correo,
    string Contrasena);