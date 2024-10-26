using Application.Dtos;
using Domain;
using Riok.Mapperly.Abstractions;

namespace Application.Mappers;

[Mapper]
public static partial class TagMapper
{
    [MapperIgnoreSource(nameof(Tag.Id))]
    [MapperIgnoreSource(nameof(Tag.BlogPosts))]
    public static partial TagDto ToTagDto(Tag tag);
}