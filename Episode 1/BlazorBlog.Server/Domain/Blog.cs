namespace BlazorBlog.Server.Domain;

public class Blog
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;
}