using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Clientes;

namespace Sistema.Datos.Mapping.Clientes
{
    public class VMClientMap : IEntityTypeConfiguration<VMClient>
    {
        public void Configure(EntityTypeBuilder<VMClient> builder)
        {
            builder.ToTable("vmclient")
                .HasKey(c => c.idclient);
        }
    }
}
