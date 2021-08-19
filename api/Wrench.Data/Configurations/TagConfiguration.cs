using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wrench.Domain.Entities;

namespace Wrench.Data.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(x => x.IdTag);

            builder
                .HasIndex(x => x.Nome)
                .IsUnique();

            builder
                .Property(x => x.Nome)
                .IsRequired();

            builder.Property(x => x.Ativo)
                .IsRequired();
        }
    }
}
