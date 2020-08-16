using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Consumibles;

namespace Sistema.Datos.Mapping.Consumibles
{
    public class SQLFamilyMap : IEntityTypeConfiguration<SQLFamily>
    {
        public void Configure(EntityTypeBuilder<SQLFamily> builder)
        {
            builder.ToTable("sqlfamily")
                .HasKey(c => c.idsql);
        }
    }
}
