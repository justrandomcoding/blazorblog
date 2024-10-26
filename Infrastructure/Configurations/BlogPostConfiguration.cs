using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal sealed class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
{
    public void Configure(EntityTypeBuilder<BlogPost> builder)
    {
        builder.ToTable("BlogPosts");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();
        builder.Property(x => x.Title).HasMaxLength(256).IsRequired();
        builder.Property(x => x.Slug).HasMaxLength(256).IsRequired();
        builder.Property(x => x.Content).IsRequired();
        builder.Property(x => x.Image).HasMaxLength(1024).IsRequired();
        builder.Property(x => x.Introduction).IsRequired();
        
        builder.HasOne(b => b.Category)
            .WithMany(c => c.BlogPosts);
        
        builder.HasOne(p => p.PostView)
            .WithOne(b => b.BlogPost)
            .HasForeignKey<PostView>(b => b.Id);
    }
}