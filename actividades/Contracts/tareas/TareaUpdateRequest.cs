namespace actividades.Contracts.tareas
{
    public class TareaUpdateRequest
    {
        public int Orden { get; set; }
        public int Prioridad { get; set; }

        public bool Completada { get; set; }
    }
}
