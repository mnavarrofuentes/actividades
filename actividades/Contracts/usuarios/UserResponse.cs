namespace actividades.Contracts.usuarios;

public record UserResponse(
    int Id,
    string Nombre,
    string Correo);