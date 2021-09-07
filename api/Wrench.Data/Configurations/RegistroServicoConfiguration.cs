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
            builder.Property(x => x.Prazo).IsRequired();
            builder.Property(x => x.ValorEstimado).IsRequired();

            builder.HasOne(x => x.Demanda).WithMany(x => x.RegistroServicos).HasForeignKey(x => x.IdDemanda);
            builder.HasOne(x => x.Demandante).WithMany().HasForeignKey(x => x.IdDemandante).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Prestador).WithMany().HasForeignKey(x => x.IdPrestador).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
