using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Consumibles;

namespace Sistema.Datos.Mapping.Consumibles
{
    public class NetworkBondMap : IEntityTypeConfiguration<NetworkBond>
    {
        public void Configure(EntityTypeBuilder<NetworkBond> builder)
        {
            builder.ToTable("networkbond")
                .HasKey(c => c.idnw);
        }
    }
}
