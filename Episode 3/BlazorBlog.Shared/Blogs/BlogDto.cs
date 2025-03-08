namespace BlazorBlog.Shared.Blogs;

public class BlogDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;
}