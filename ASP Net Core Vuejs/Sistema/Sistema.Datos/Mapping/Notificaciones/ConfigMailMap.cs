using Microsoft.EntityFrameworkCore;
using Sistema.Entidades.Notificaciones;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sistema.Datos.Mapping.Notificaciones
{
    public class ConfigMailMap : IEntityTypeConfiguration<ConfigMail>
    {
        public void Configure(EntityTypeBuilder<ConfigMail> builder)
        {
            builder.ToTable("configmail")
                .HasKey(c => c.idperiodo);
        }
    }
}
