using Application.Categories;
using Application.Dtos;
using Domain;
using Riok.Mapperly.Abstractions;

namespace Application.Mappers;

[Mapper]
public static partial class CategoryMapper
{
    [MapperIgnoreSource(nameof(Category.ShowOnNavBar))]
    [MapperIgnoreSource(nameof(Category.BlogPosts))]
    public static partial CategoryDto ToCategoryDto(Category category);
}