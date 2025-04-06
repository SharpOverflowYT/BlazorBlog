using BlazorBlog.Shared.Blogs;
using BlazorBlog.Shared.Blogs.Commands;

namespace BlazorBlog.Server.Slices.Blogs;

public interface IBlogService
{
    Task<List<BlogDto>> GetAllAsync();

    Task<BlogDto?> GetByIdAsync(Guid id);

    Task<Guid> AddAsync(AddOrUpdateBlogCommand command);

    Task<bool> UpdateAsync(AddOrUpdateBlogCommand command);

    Task<bool> DeleteAsync(Guid id);
}