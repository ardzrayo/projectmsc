using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Sistema.Entidades.Notificaciones;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sistema.Datos.Mapping.Notificaciones
{
    public class PeriodoMap : IEntityTypeConfiguration<Periodo>
    {
        public void Configure(EntityTypeBuilder<Periodo> builder)
        {
            builder.ToTable("periodo")
                .HasKey(c => c.idperiodo);
        }
    }
}
