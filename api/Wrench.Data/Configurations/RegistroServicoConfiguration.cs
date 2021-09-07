using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wrench.Domain.Entities;

namespace Wrench.Data.Configurations
{
    public class RegistroServicoConfiguration : IEntityTypeConfiguration<RegistroServico>
    {
        public void Configure(EntityTypeBuilder<RegistroServico> builder)
        {
            builder.HasKey(x => x.IdRegistroServico);

            builder.Property(x => x.Estado).IsRequired();

            builder.HasOne(x => x.Demanda).WithOne().HasForeignKey<RegistroServico>(x => x.IdDemanda);
        }
    }
}
