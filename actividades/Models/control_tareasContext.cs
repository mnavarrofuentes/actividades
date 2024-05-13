using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace actividades.Models
{
    public partial class control_tareasContext : DbContext
    {
        public control_tareasContext()
        {
        }

        public control_tareasContext(DbContextOptions<control_tareasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AsignacionEquipo> AsignacionEquipos { get; set; } = null!;
        public virtual DbSet<AsignacionUsuario> AsignacionUsuarios { get; set; } = null!;
        public virtual DbSet<Comentario> Comentarios { get; set; } = null!;
        public virtual DbSet<Equipo> Equipos { get; set; } = null!;
        public virtual DbSet<MiembroEquipo> MiembroEquipos { get; set; } = null!;
        public virtual DbSet<Tarea> Tareas { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=control_tareas;Username=postgres;Password=marquense");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AsignacionEquipo>(entity =>
            {
                entity.ToTable("asignacion_equipo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EquipoId).HasColumnName("equipo_id");

                entity.Property(e => e.TareaId).HasColumnName("tarea_id");

                entity.HasOne(d => d.Equipo)
                    .WithMany(p => p.AsignacionEquipos)
                    .HasForeignKey(d => d.EquipoId)
                    .HasConstraintName("asignacion_equipo_equipo_id_fkey");

                entity.HasOne(d => d.Tarea)
                    .WithMany(p => p.AsignacionEquipos)
                    .HasForeignKey(d => d.TareaId)
                    .HasConstraintName("asignacion_equipo_tarea_id_fkey");
            });

            modelBuilder.Entity<AsignacionUsuario>(entity =>
            {
                entity.ToTable("asignacion_usuario");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TareaId).HasColumnName("tarea_id");

                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

                entity.HasOne(d => d.Tarea)
                    .WithMany(p => p.AsignacionUsuarios)
                    .HasForeignKey(d => d.TareaId)
                    .HasConstraintName("asignacion_usuario_tarea_id_fkey");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.AsignacionUsuarios)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("asignacion_usuario_usuario_id_fkey");
            });

            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.ToTable("comentario");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comentario1).HasColumnName("comentario");

                entity.Property(e => e.Fecha)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.TareaId).HasColumnName("tarea_id");

                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

                entity.HasOne(d => d.Tarea)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(d => d.TareaId)
                    .HasConstraintName("comentario_tarea_id_fkey");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("comentario_usuario_id_fkey");
            });

            modelBuilder.Entity<Equipo>(entity =>
            {
                entity.ToTable("equipo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<MiembroEquipo>(entity =>
            {
                entity.ToTable("miembro_equipo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EquipoId).HasColumnName("equipo_id");

                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

                entity.HasOne(d => d.Equipo)
                    .WithMany(p => p.MiembroEquipos)
                    .HasForeignKey(d => d.EquipoId)
                    .HasConstraintName("miembro_equipo_equipo_id_fkey");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.MiembroEquipos)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("miembro_equipo_usuario_id_fkey");
            });

            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.ToTable("tarea");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Completada)
                    .HasColumnName("completada")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.CreadorId).HasColumnName("creador_id");

                entity.Property(e => e.Descripcion).HasColumnName("descripcion");

                entity.Property(e => e.FechaLimite).HasColumnName("fecha_limite");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Prioridad)
                    .HasColumnName("prioridad")
                    .HasDefaultValueSql("0");

                entity.HasOne(d => d.Creador)
                    .WithMany(p => p.Tareas)
                    .HasForeignKey(d => d.CreadorId)
                    .HasConstraintName("tarea_creador_id_fkey");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario");

                entity.HasIndex(e => e.Correo, "usuario_correo_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(100)
                    .HasColumnName("contrasena");

                entity.Property(e => e.Correo)
                    .HasMaxLength(100)
                    .HasColumnName("correo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
