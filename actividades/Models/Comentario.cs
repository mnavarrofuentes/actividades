using System;
using System.Collections.Generic;

namespace actividades.Models
{
    public partial class Comentario
    {
        public int Id { get; set; }
        public string Comentario1 { get; set; } = null!;
        public int? UsuarioId { get; set; }
        public int? TareaId { get; set; }
        public DateTime? Fecha { get; set; }

        public virtual Tarea? Tarea { get; set; }
        public virtual Usuario? Usuario { get; set; }
    }
}
