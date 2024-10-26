using Application.Dtos;
using Application.Mappers;
using Domain;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BlogPosts.List;


public class ListBlogPostsByTagRequest : PaginatedQuery, IRequest<PaginatedResponse<BlogPostDto>>
{
    public required string TagSlug { get; init; }
}
public class ListBlogPostsByTagHandler(BlogDbContext blogDbContext) : IRequestHandler<ListBlogPostsByTagRequest, PaginatedResponse<BlogPostDto>>
{
    public async Task<PaginatedResponse<BlogPostDto>> Handle(ListBlogPostsByTagRequest request, CancellationToken cancellationToken)
    {
        var blogPostQuery = blogDbContext.BlogPosts
            .Where(b => b.Tags.Select(t => t.Slug).Contains(request.TagSlug) && b.IsPublished && !b.IsDeleted);
        
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