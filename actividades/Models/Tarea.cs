using System;
using System.Collections.Generic;

namespace actividades.Models
{
    public partial class Tarea
    {
        public Tarea()
        {
            AsignacionEquipos = new HashSet<AsignacionEquipo>();
            AsignacionUsuarios = new HashSet<AsignacionUsuario>();
            Comentarios = new HashSet<Comentario>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public DateOnly? FechaLimite { get; set; }
        public int? Orden { get; set; }
        public bool? Completada { get; set; }
        public int? CreadorId { get; set; }
        public int Prioridad { get; set; }

        public virtual Usuario? Creador { get; set; }
        public virtual ICollection<AsignacionEquipo> AsignacionEquipos { get; set; }
        public virtual ICollection<AsignacionUsuario> AsignacionUsuarios { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }
    }
}
