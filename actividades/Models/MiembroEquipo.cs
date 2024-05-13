using System;
using System.Collections.Generic;

namespace actividades.Models
{
    public partial class MiembroEquipo
    {
        public int Id { get; set; }
        public int? UsuarioId { get; set; }
        public int? EquipoId { get; set; }

        public virtual Equipo? Equipo { get; set; }
        public virtual Usuario? Usuario { get; set; }
    }
}
