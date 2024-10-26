using Application.BlogPosts;
using Application.Dtos;
using Domain;
using Riok.Mapperly.Abstractions;

namespace Application.Mappers;

[Mapper]
[UseStaticMapper(typeof(CategoryMapper))]
[UseStaticMapper(typeof(TagMapper))]
public static partial class BlogPostMapper
{
    [MapperIgnoreSource(nameof(BlogPost.Id))]
    [MapperIgnoreSource(nameof(BlogPost.IsPublished))]
    [MapperIgnoreSource(nameof(BlogPost.IsDeleted))]
    [MapperIgnoreSource(nameof(BlogPost.PostView))]
    public static partial BlogPostDto ToBlogPostDto(BlogPost blogPost);
}