using Application.Dtos;
using Application.Mappers;
using Domain;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BlogPosts.List;

public class ListBlogPostsByCategoryRequest : PaginatedQuery, IRequest<PaginatedResponse<BlogPostDto>>
{
    public required string CategorySlug { get; init; }
}


public class ListBlogPostsByCategoryHandler(BlogDbContext blogDbContext) : IRequestHandler<ListBlogPostsByCategoryRequest, PaginatedResponse<BlogPostDto>>
{
    public async Task<PaginatedResponse<BlogPostDto>> Handle(ListBlogPostsByCategoryRequest request, CancellationToken cancellationToken)
    {
        var blogPostQuery = blogDbContext.BlogPosts
            .Where(b => b.Category.Slug.Equals(request.CategorySlug) && b.IsPublished && !b.IsDeleted);
        
        var blogPosts = await blogPostQuery
            .Include(nameof(BlogPost.Tags))
            .OrderByDescending(b => b.PublishDate)
            .Skip(request.PageIndex * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var count = await blogPostQuery.CountAsync(cancellationToken);
        
        return new PaginatedResponse<BlogPostDto>(
            request.PageIndex, 
            request.PageSize, 
            count, 
            blogPosts.Select(BlogPostMapper.ToBlogPostDto));
    }
}