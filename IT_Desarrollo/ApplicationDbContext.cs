using IT_Desarrollo_Back.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GusticosWebAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación Usuario - Rol (muchos a uno)
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany()
                .HasForeignKey(u => u.RolId);

            // Relación Respuesta - Usuario (muchos a uno)
            modelBuilder.Entity<Respuesta>()
                .HasOne(r => r.Usuario)
                .WithMany()
                .HasForeignKey(r => r.UsuarioId);

            // Relación Respuesta - Pregunta (muchos a uno)
            modelBuilder.Entity<Respuesta>()
                .HasOne(r => r.Pregunta)
                .WithMany()
                .HasForeignKey(r => r.PreguntaId);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Usuario> tbl_usuarios => Set<Usuario>();
        public DbSet<Rol> tbl_roles => Set<Rol>();
        public DbSet<Respuesta> tbl_respuestas => Set<Respuesta>();
        public DbSet<Pregunta> tbl_preguntas => Set<Pregunta>();

    }
}
