using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IT_Desarrollo_Back.Entidades.Configuraciones
{
    public class RespuestaConfig : IEntityTypeConfiguration<Respuesta>
    {
        public void Configure(EntityTypeBuilder<Respuesta> builder)
        {
            builder.HasKey(r => r.pkid);
            builder.Property(r => r.pregunta).HasMaxLength(100);
            builder.Property(r => r.respuesta).HasMaxLength(150);
        }

    }
}
