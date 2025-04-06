using BlazorBlog.Shared.Blogs;
using BlazorBlog.Shared.Blogs.Commands;

namespace BlazorBlog.Server.Slices.BlogPosts;

public interface IBlogPostService
{
    Task<BlogPostDto?> GetByIdAsync(Guid blogId, Guid id);

    Task<List<BlogPostDto>> GetAllAsync(Guid blogId);

    Task<Guid> AddAsync(Guid blogId, AddOrUpdateBlogPostCommand command);

    Task<bool> UpdateAsync(Guid blogId, AddOrUpdateBlogPostCommand command);

    Task<bool> DeleteAsync(Guid blogId, Guid id);
}