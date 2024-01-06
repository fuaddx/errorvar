using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Core.Entities;

namespace Twitter.Dal.Configurations
{
    public class BlogsConfiguration:IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            
            builder.Property(x => x.Content).IsRequired()
                .HasMaxLength(50);
            builder.Property(x=>x.Author).IsRequired()
                .HasMaxLength(20);
            builder.Property(x => x.CreatedTime).IsRequired()
                .HasDefaultValue(DateTime.UtcNow);
            builder.Property(x=>x.UpdatedCount).IsRequired()
                .HasDefaultValue(0);
            builder.Property(x => x.Title).IsRequired()
                .HasMaxLength(256);

        }
    }
}
