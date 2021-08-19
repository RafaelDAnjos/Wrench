using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wrench.Domain.Entities;

namespace Wrench.Data.Configurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(x => x.IdChat);

            builder.Property(x => x.Titulo).HasMaxLength(150).IsRequired();
            builder.Property(x => x.CriadoEm).IsRequired();
            builder.Property(x => x.Ativo).IsRequired();
        }
    }
}
