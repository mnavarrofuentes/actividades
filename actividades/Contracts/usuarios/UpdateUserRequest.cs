namespace actividades.Contracts.usuarios;

public record UpdateUserRequest(
    string Nombre,
    string Correo,
    string Contrasena);