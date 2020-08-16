using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Servidores;

namespace Sistema.Datos.Mapping.Servidores
{
    public class PoolsMap : IEntityTypeConfiguration<Pools>
    {
        public void Configure(EntityTypeBuilder<Pools> builder)
        {
            builder.ToTable("pools")
                .HasKey(c => c.idpool);
        }
    }
}
