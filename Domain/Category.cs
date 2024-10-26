namespace Domain;

public class Category
{
    public Guid Id { get; set; }
    
    public required string Name { get; set; }
    
    public required string Slug { get; set; }
    
    public bool ShowOnNavBar { get; set; }
    
    public ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
}