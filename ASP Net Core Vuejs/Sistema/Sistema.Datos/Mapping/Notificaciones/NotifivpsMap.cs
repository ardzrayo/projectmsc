using Microsoft.EntityFrameworkCore;
using Sistema.Entidades.Notificaciones;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sistema.Datos.Mapping.Notificaciones
{
    public class NotifivpsMap : IEntityTypeConfiguration<Notifivps>
    {
        public void Configure(EntityTypeBuilder<Notifivps> builder)
        {
            builder.ToTable("notifivps")
                .HasKey(c => c.idnotivps);
        }
    }
}
