using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Consumibles;

namespace Sistema.Datos.Mapping.Consumibles
{
    public class OSFamilyMap : IEntityTypeConfiguration<OSFamily>
    {
        public void Configure(EntityTypeBuilder<OSFamily> builder)
        {
            builder.ToTable("osfamily")
                .HasKey(c => c.idos);
        }
    }
}
