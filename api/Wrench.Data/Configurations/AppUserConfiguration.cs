using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wrench.Domain.Entities.Identity;

namespace Wrench.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasMany(x => x.Tags).WithMany(x => x.AtribuidosPara).UsingEntity(x => x.ToTable("TagUsers"));
        }
    }
}
