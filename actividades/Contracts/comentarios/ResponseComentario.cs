namespace actividades.Contracts.comentarios
{
    public record ResponseComentario
    {
        public int Id { get; init; }
        public string Contenido { get; init; }
        public string UsuarioEmail { get; init; }
    }
}
