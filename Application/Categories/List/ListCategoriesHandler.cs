using Application.Dtos;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories.List;

public record ListCategoriesRequest() : IRequest<IEnumerable<CategoryDto>>;

public class ListCategoriesHandler(BlogDbContext blogDbContext) : IRequestHandler<ListCategoriesRequest, IEnumerable<CategoryDto>>
{
    public async Task<IEnumerable<CategoryDto>> Handle(ListCategoriesRequest request, CancellationToken cancellationToken)
    {
        var categories = await blogDbContext.Categories
            .Where(c => c.ShowOnNavBar)
            .ToListAsync(cancellationToken);
        
        return categories.Select(Mappers.CategoryMapper.ToCategoryDto);
    }
}