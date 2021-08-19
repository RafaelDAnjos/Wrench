using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Wrench.Domain.Entities;
using Wrench.Domain.Entities.Identity;

namespace Wrench.Data.Context
{
    public class WrenchDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DbSet<Tag> Tag { get; set; }

        public WrenchDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(WrenchDbContext).Assembly);

            base.OnModelCreating(builder);
        }
    }
}