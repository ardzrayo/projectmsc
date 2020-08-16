using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Notificaciones;

namespace Sistema.Datos.Mapping.Notificaciones
{
    public class NotifiHistoMap : IEntityTypeConfiguration<NotifiHisto>
    {
        public void Configure(EntityTypeBuilder<NotifiHisto> builder)
        {
            builder.ToTable("notifihisto")
                .HasKey(c => c.idnotihisto);
        }
    }
}
