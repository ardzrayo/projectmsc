using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Consumibles;

namespace Sistema.Datos.Mapping.Consumibles
{
    public class OSVersionMap : IEntityTypeConfiguration<OSVersion>
    {
        public void Configure(EntityTypeBuilder<OSVersion> builder)
        {
            builder.ToTable("osversion")
                .HasKey(c => c.idversion);
        }
    }
}
