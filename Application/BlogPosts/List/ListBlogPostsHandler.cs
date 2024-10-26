using Application.Dtos;
using Application.Mappers;
using Domain;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.BlogPosts.List;

public class ListBlogPostsRequest : PaginatedQuery, IRequest<PaginatedResponse<BlogPostDto>>
{
    public bool ShowDeleted { get; init; }
    public bool ShowPublished { get; init; }
}

public class ListBlogPostsHandler(BlogDbContext blogDbContext)
    : IRequestHandler<ListBlogPostsRequest, PaginatedResponse<BlogPostDto>>
{
    public async Task<PaginatedResponse<BlogPostDto>> Handle(ListBlogPostsRequest request,
        CancellationToken cancellationToken)
    {
        var blogPostQuery = blogDbContext.BlogPosts
            .Where(b => b.IsPublished == request.ShowPublished && b.IsDeleted == request.ShowDeleted);

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