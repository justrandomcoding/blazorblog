using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class BlogDbContext: DbContext
{
    public BlogDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<BlogPost> BlogPosts { get; set; }
    
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<Tag> Tags { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogDbContext).Assembly);
    }
}