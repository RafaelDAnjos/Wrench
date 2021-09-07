using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wrench.Domain.Entities;

namespace Wrench.Data.Configurations
{
    public class AvaliacaoConfiguration : IEntityTypeConfiguration<Avaliacao>
    {
        public void Configure(EntityTypeBuilder<Avaliacao> builder)
        {
            builder.HasKey(x => x.IdAvaliacao);

            builder.HasOne(x => x.RegistroServico).WithMany().HasForeignKey(x => x.IdRegistroServico).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Usuario).WithMany().HasForeignKey(x => x.IdUsuario);

            builder.Property(x => x.ValorNota).IsRequired();
            builder.Property(x => x.EnviadoEm).IsRequired();
        }
    }
}