namespace BlazorBlog.Shared.Blogs.Commands;

public class AddOrUpdateBlogCommand
{
    public Guid? Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;
}