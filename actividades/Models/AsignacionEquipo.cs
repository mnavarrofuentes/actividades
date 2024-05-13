using System;
using System.Collections.Generic;

namespace actividades.Models
{
    public partial class AsignacionEquipo
    {
        public int Id { get; set; }
        public int? TareaId { get; set; }
        public int? EquipoId { get; set; }

        public virtual Equipo? Equipo { get; set; }
        public virtual Tarea? Tarea { get; set; }
    }
}
