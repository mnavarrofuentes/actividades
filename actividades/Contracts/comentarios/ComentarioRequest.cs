namespace actividades.Contracts.comentarios
{
    public class ComentarioRequest
    {
        public int TareaId { get; set; }
        public string Contenido { get; set; }

        public int UsuarioId { get; set; }
    }
}
