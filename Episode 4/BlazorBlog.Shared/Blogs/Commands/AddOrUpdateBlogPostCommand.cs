namespace BlazorBlog.Shared.Blogs.Commands;

public class AddOrUpdateBlogPostCommand
{
    public Guid? Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;
}