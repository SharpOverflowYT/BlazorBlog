using BlazorBlog.Server.Domain;

namespace BlazorBlog.Server.Slices.BlogPosts.Entities;

public class BlogPost
{
    public Guid Id { get; set; }

    public Guid BlogId { get; set; }

    public Blog Blog { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
}