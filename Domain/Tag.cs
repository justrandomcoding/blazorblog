namespace Domain;

public class Tag
{
    public Guid Id { get; set; }
    
    public required string Name { get; set; }
    
    public required string Slug { get; set; }
    
    public ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
}