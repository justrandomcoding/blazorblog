namespace Domain;

public class BlogPost
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Slug { get; set; }
    public required string Image { get; set; }
    public required string Introduction { get; set; }
    public required string Content { get; set; }
    
    public DateTimeOffset? PublishDate { get; set; }
    
    public bool IsPublished { get; set; }
    
    public bool IsDeleted { get; set; }

    public virtual required  Category Category { get; set; }
    
    public virtual  PostView? PostView { get; set; }

    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
}