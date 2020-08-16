using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Consumibles;

namespace Sistema.Datos.Mapping.Consumibles
{
    public class VMTypeMap : IEntityTypeConfiguration<VMType>
    {
        public void Configure(EntityTypeBuilder<VMType> builder)
        {
            builder.ToTable("vmtype")
                .HasKey(c => c.idvmtype);
        }
    }
}
