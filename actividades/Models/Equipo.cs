using System;
using System.Collections.Generic;

namespace actividades.Models
{
    public partial class Equipo
    {
        public Equipo()
        {
            AsignacionEquipos = new HashSet<AsignacionEquipo>();
            MiembroEquipos = new HashSet<MiembroEquipo>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<AsignacionEquipo> AsignacionEquipos { get; set; }
        public virtual ICollection<MiembroEquipo> MiembroEquipos { get; set; }
    }
}
