using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Servidores;

namespace Sistema.Datos.Mapping.Servidores
{
    public class VPSMap : IEntityTypeConfiguration<VPS>
    {
        public void Configure(EntityTypeBuilder<VPS> builder)
        {
            builder.ToTable("vps")
                .HasKey(c => c.idvps);
        }
    }
}
