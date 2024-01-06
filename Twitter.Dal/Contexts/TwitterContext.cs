using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Twitter.Core.Entities;
using Twitter.Core.Entity;
using Twitter.Core.Entity.Common;

namespace Twitter.Dal.Contexts
{
    public class TwitterContext : IdentityDbContext<AppUser>
    {
        public TwitterContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Topic>Topics { get; set; }
        public DbSet<AppUser>AppUsers { get; set; }
        public DbSet<Blog>Blogs { get; set; }
        public DbSet<Files>Files { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                    entry.Entity.CreatedTime = DateTime.UtcNow;
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Configure many-to-many relationship between Blog and Topic using BlogTopic as the join entity
            modelBuilder.Entity<BlogTopic>()
                .HasKey(bt => new { bt.BlogId, bt.TopicId });

            modelBuilder.Entity<BlogTopic>()
                .HasOne(bt => bt.Blog)
                .WithMany(b => b.BlogTopics)
                .HasForeignKey(bt => bt.BlogId);

            modelBuilder.Entity<BlogTopic>()
                .HasOne(bt => bt.Topic)
                .WithMany(t => t.BlogTopics)
                .HasForeignKey(bt => bt.TopicId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
