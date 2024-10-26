using Domain;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;


public record SeedCommand : IRequest<Unit>;

public sealed class SeedCommandHandler(BlogDbContext blogDbContext) : IRequestHandler<SeedCommand, Unit>
{
    public async Task<Unit> Handle(SeedCommand request, CancellationToken cancellationToken)
    {
        await ClearDatabase(cancellationToken);
        
        var categoriesVisible = await AddCategories(cancellationToken);
        
        var tags =  await AddTags(cancellationToken);
        
        await AddBlogPosts(categoriesVisible, tags, cancellationToken);

        return Unit.Value;
    }

    private async Task ClearDatabase(CancellationToken cancellationToken)
    {
        await blogDbContext.Database.ExecuteSqlRawAsync("DELETE FROM [BlogPostTag];", cancellationToken);
        await blogDbContext.Database.ExecuteSqlRawAsync("DELETE FROM [BlogPosts];", cancellationToken);
        await blogDbContext.Database.ExecuteSqlRawAsync("DELETE FROM [Tags]", cancellationToken);
        await blogDbContext.Database.ExecuteSqlRawAsync("DELETE FROM [Categories]", cancellationToken);
    }

    private async Task<List<BlogPost>> AddBlogPosts(
        List<Category> categories,
        List<Tag> tags,
        CancellationToken cancellationToken)
    {
        var result = new List<BlogPost>();

        for (int i = 0; i < 20; i++)
        {
            var blogPost = new BlogPost
            {
                Title = $"Article {i}",
                Content = $"Content {i}",
                Image = $"Image {i}",
                Introduction = $"Introduction {i}",
                Slug = $"Article-{i}",
                IsDeleted = false,
                IsPublished = i % 5 != 0,
                Category = categories[i % categories.Count],
                Tags = Random.Shared.GetItems(tags.ToArray(), 2),
                PublishDate = DateTime.Now.AddDays(-i),
            };
            await blogDbContext.BlogPosts.AddAsync(blogPost, cancellationToken);
        }

        await blogDbContext.SaveChangesAsync(cancellationToken);

        return result;
    }

    private async Task<List<Tag>> AddTags(CancellationToken cancellationToken)
    {
        var result = new List<Tag>();
        
        for (int i = 0; i < 10; i++)
        {
            var tag = new Tag
            {
                Name = $"Tag {i}",
                Slug = $"Tag-{i}"
            };
            
            result.Add(tag);
            await blogDbContext.Tags.AddAsync(tag, cancellationToken);
        }

        await blogDbContext.SaveChangesAsync(cancellationToken);

        return result;
    }
    
    private async Task<List<Category>> AddCategories(CancellationToken cancellationToken)
    {
        var result = new List<Category>();
        
        for (int i = 0; i < 10; i++)
        {
            var category = new Category
            {
                Name = $"Category {i}",
                Slug = $"Category-{i}",
                ShowOnNavBar = i % 2 == 0, 
            };
            
            if (category.ShowOnNavBar)
            {
                result.Add(category);
            }

            await blogDbContext.Categories.AddAsync(category, cancellationToken);
        }

        await blogDbContext.SaveChangesAsync(cancellationToken);

        return result;
    }
}
