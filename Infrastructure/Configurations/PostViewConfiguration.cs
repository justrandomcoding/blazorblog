using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal sealed class PostViewConfiguration : IEntityTypeConfiguration<PostView>
{
    public void Configure(EntityTypeBuilder<PostView> builder)
    {
        builder.ToTable("PostViews");
    }
}