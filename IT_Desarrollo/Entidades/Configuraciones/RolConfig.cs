using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IT_Desarrollo_Back.Entidades.Configuraciones
{
    public class RolConfig : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.HasKey(r => r.pkid);
            builder.Property(r => r.descripcion);
        }
    }
}
