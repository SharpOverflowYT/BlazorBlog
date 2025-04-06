using BlazorBlog.Server.Extensions;
using BlazorBlog.Server.Persistence;
using BlazorBlog.Shared.Blogs;
using BlazorBlog.Shared.Blogs.Commands;
using Microsoft.EntityFrameworkCore;

namespace BlazorBlog.Server.Slices.Blogs;

public class BlogService(ILogger<BlogService> logger, BlogDataContext blogDataContext) : IBlogService
{
    public async Task<List<BlogDto>> GetAllAsync()
    {
        try
        {
            logger.LogInformation("Getting all blogs");

            var blogs = await blogDataContext.Blogs
                .ProjectToDtos()
                .ToListAsync();

            logger.LogInformation("{BlogCount} blogs retrieved", blogs.Count);

            return blogs;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Failed to get blogs");
            return [];
        }
    }

    public async Task<BlogDto?> GetByIdAsync(Guid id)
    {
        try
        {
            logger.LogInformation("Getting blog with ID {BlogId}", id);

            var blog = await blogDataContext.Blogs
                .ProjectToDtos()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (blog == null)
            {
                logger.LogWarning("Blog with ID {BlogId} not found", id);
                return null;
            }

            logger.LogInformation("Blog with ID {BlogId} retrieved", id);

            return blog;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Failed to get blog with ID {BlogId}", id);
            return null;
        }
    }

    public async Task<Guid> AddAsync(AddOrUpdateBlogCommand command)
    {
        try
        {
            logger.LogInformation("Adding new blog");

            var blog = command.MapToNewBlog();

            blog.Id = Guid.NewGuid();

            blogDataContext.Blogs.Add(blog);
            await blogDataContext.SaveChangesAsync();

            logger.LogInformation("Blog with ID {BlogId} added", blog.Id);

            return blog.Id;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Failed to add blog");
            throw;
        }
    }

    public async Task<bool> UpdateAsync(AddOrUpdateBlogCommand command)
    {
        try
        {
            logger.LogInformation("Updating blog with ID {BlogId}", command.Id);

            var existingBlog = await blogDataContext.Blogs.FindAsync(command.Id);

            if (existingBlog == null)
            {
                logger.LogWarning("Blog with ID {BlogId} not found", command.Id);
                return false;
            }

            command.MapToExistingBlog(existingBlog);

            await blogDataContext.SaveChangesAsync();

            logger.LogInformation("Blog with ID {BlogId} updated", command.Id);

            return true;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Failed to update blog with ID {BlogId}", command.Id);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            logger.LogInformation("Deleting blog with ID {BlogId}", id);

            var existingBlog = await blogDataContext.Blogs.FindAsync(id);

            if (existingBlog == null)
            {
                logger.LogWarning("Blog with ID {BlogId} not found", id);
                return false;
            }

            blogDataContext.Blogs.Remove(existingBlog);
            await blogDataContext.SaveChangesAsync();

            logger.LogInformation("Blog with ID {BlogId} deleted", id);

            return true;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Failed to delete blog with ID {BlogId}", id);
            return false;
        }
    }
}