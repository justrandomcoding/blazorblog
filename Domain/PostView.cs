namespace Domain;

public class PostView
{
    public Guid Id { get; set; }
    
    public int Count { get; set; }
    public virtual BlogPost? BlogPost { get; set; }
    
    public DateTimeOffset LastView { get; set; }
}