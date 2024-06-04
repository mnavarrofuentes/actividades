using actividades.common;
using System.Text.Json.Serialization;

namespace actividades.Contracts.tareas;
public class TareaRequest
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    [JsonConverter(typeof(DateOnlyConverter))]
    public DateOnly FechaLimite { get; set; }
    public int Orden { get; set; }
    public bool Completada { get; set; }
    public int CreadorId { get; set; }

    public int Prioridad { get; set; }

    public int GrupoId { get; set; }

    public int ResponsableId { get; set; }
}
