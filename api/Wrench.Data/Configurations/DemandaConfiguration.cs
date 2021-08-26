using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wrench.Domain.Entities;

namespace Wrench.Data.Configurations
{
    public class DemandaConfiguration : IEntityTypeConfiguration<Demanda>
    {
        public void Configure(EntityTypeBuilder<Demanda> builder)
        {
            builder.HasKey(x => x.IdDemanda);

            builder.Property(x => x.Titulo).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Descricao).HasColumnType("nvarchar(max)");
            builder.Property(x => x.Estado).IsRequired();
            builder.HasOne(x => x.Demandante).WithMany().HasForeignKey(x => x.IdDemandante);

            builder.HasMany(x => x.Tags).WithMany(x => x.Demandas).UsingEntity(x => x.ToTable("TagDemandas"));
        }
    }
}
