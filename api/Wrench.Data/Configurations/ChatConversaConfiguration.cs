using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wrench.Domain.Entities;

namespace Wrench.Data.Configurations
{
    public class ChatConversaConfiguration : IEntityTypeConfiguration<ChatConversa>
    {
        public void Configure(EntityTypeBuilder<ChatConversa> builder)
        {
            builder.HasKey(x => x.IdChatConversa);

            builder.HasOne(x => x.Chat).WithMany(x => x.Mensagens).HasForeignKey(x => x.IdChat);
            builder.HasOne(x => x.UserDe).WithMany().HasForeignKey(x => x.De).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.UserPara).WithMany().HasForeignKey(x => x.Para).OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Mensagem)
                .HasColumnType("nvarchar(max)")
                .IsRequired();
        }
    }
}
