using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Consumibles;

namespace Sistema.Datos.Mapping.Consumibles
{
    public class SQLVersionMap : IEntityTypeConfiguration<SQLVersion>
    {
        public void Configure(EntityTypeBuilder<SQLVersion> builder)
        {
            builder.ToTable("sqlversion")
                .HasKey(c => c.idsqlversion);
        }
    }
}
