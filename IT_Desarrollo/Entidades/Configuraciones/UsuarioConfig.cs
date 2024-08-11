using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IT_Desarrollo_Back.Entidades.Configuraciones
{
    public class UsuarioConfig:IEntityTypeConfiguration<Usuario>
    {
        public void Configure (EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.pkid);
            builder.Property(u => u.nombre).HasMaxLength(100);
            builder.Property(u => u.apellido).HasMaxLength(100);
            builder.Property(u => u.email).HasMaxLength(100);
            builder.Property(u => u.contrasena).HasMaxLength(30);

        }
    }
}
