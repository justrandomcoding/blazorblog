namespace Application.Dtos;

public record BlogPostDto(
    string Title,
    string Slug,
    string Introduction,
    string Content,
    string Image,
    DateTimeOffset PublishDate,
    CategoryDto Category,
    TagDto[] Tags);
