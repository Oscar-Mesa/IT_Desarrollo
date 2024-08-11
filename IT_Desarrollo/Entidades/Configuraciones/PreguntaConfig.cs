using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IT_Desarrollo_Back.Entidades.Configuraciones
{
    public class PreguntaConfig : IEntityTypeConfiguration<Pregunta>
    {
        public void Configure(EntityTypeBuilder<Pregunta> builder)
        {
            builder.HasKey(p => p.pkid);
            builder.Property(p => p.descripcion).HasMaxLength(100);
        }
    }
}
