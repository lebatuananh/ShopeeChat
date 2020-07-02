using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShopeeChat.WebApi.Data.Entities;
using ShopeeChat.WebApi.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShopeeChat.WebApi.Data
{
    public class ShopeeChatDbContext : IdentityDbContext<Membership>
    {
        public ShopeeChatDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().Property(x => x.Id).HasMaxLength(50).IsUnicode(false);
            builder.Entity<Membership>().Property(x => x.Id).HasMaxLength(50).IsUnicode(false);
            builder.Entity<Permission>()
                .HasKey(c => new { c.RoleId, c.FunctionId, c.CommandId });
            builder.Entity<CommandInFunction>()
                .HasKey(c => new { c.CommandId, c.FunctionId });
            builder.Entity<ShopeeUserTagMapping>().HasKey(x => new { x.TagId, x.ShopeeUserId });

            builder.HasSequence("ShopeeChatDBSequence");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            IEnumerable<EntityEntry> modified = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
            foreach (EntityEntry item in modified)
            {
                if (item.Entity is IDateTracking changedOrAddedItem)
                {
                    if (item.State == EntityState.Added)
                    {
                        changedOrAddedItem.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        changedOrAddedItem.LastUpdatedDate = DateTime.Now;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<ShopeeUser> ShopeeUsers { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ShopeeUserTagMapping> ShopeeUserTagMappings { get; set; }
        public DbSet<Command> Commands { set; get; }
        public DbSet<CommandInFunction> CommandInFunctions { set; get; }
        public DbSet<Function> Functions { set; get; }
        public DbSet<Permission> Permissions { set; get; }
    }
}