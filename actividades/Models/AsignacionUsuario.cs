using System;
using System.Collections.Generic;

namespace actividades.Models
{
    public partial class AsignacionUsuario
    {
        public int Id { get; set; }
        public int? TareaId { get; set; }
        public int? UsuarioId { get; set; }

        public virtual Tarea? Tarea { get; set; }
        public virtual Usuario? Usuario { get; set; }
    }
}
