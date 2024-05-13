using System;
using System.Collections.Generic;

namespace actividades.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            AsignacionUsuarios = new HashSet<AsignacionUsuario>();
            Comentarios = new HashSet<Comentario>();
            MiembroEquipos = new HashSet<MiembroEquipo>();
            Tareas = new HashSet<Tarea>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Contrasena { get; set; } = null!;

        public virtual ICollection<AsignacionUsuario> AsignacionUsuarios { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<MiembroEquipo> MiembroEquipos { get; set; }
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}
