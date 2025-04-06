using BlazorBlog.Server.Extensions;
using BlazorBlog.Server.Persistence;
using BlazorBlog.Shared.Blogs;
using BlazorBlog.Shared.Blogs.Commands;
using Microsoft.EntityFrameworkCore;

namespace BlazorBlog.Server.Slices.BlogPosts;

public class BlogPostService(ILogger<BlogPostService> logger, BlogDataContext blogDataContext) : IBlogPostService
{
    public async Task<BlogPostDto?> GetByIdAsync(Guid blogId, Guid id)
    {
        try
        {
            logger.LogInformation("Getting blog post by ID {BlogPostId}", id);
            logger.LogInformation($"Getting blog post by ID {id}");

            var post = await blogDataContext.BlogPosts
                .Where(x => x.BlogId == blogId && x.Id == id)
                .ProjectToDtos()
                .FirstOrDefaultAsync();

            if (post == null)
            {
                logger.LogWarning("Blog post with ID {BlogPostId} not found", id);
                return null;
            }

            logger.LogInformation("Blog post with ID {BlogPostId} found", id);

            return post;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error getting blog post by ID {BlogPostId}", id);
            throw;
        }
    }

    public async Task<List<BlogPostDto>> GetAllAsync(Guid blogId)
    {
        try
        {
            logger.LogInformation("Getting all blog posts for blog {BlogId}", blogId);

            var posts = await blogDataContext.BlogPosts
                .Where(x => x.BlogId == blogId)
                .ProjectToDtos()
                .ToListAsync();

            logger.LogInformation("{BlogPostCount} blog posts retrieved", posts.Count);

            return posts;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error getting all blog posts for blog {BlogId}", blogId);
            return [];
        }
    }

    public async Task<Guid> AddAsync(Guid blogId, AddOrUpdateBlogPostCommand command)
    {
        try
        {
            logger.LogInformation("Adding new blog post for blog {BlogId}", blogId);

            var post = command.MapToNewBlogPost();

            post.Id = Guid.NewGuid();
            post.BlogId = blogId;

            var now = DateTimeOffset.UtcNow;

            post.CreatedAt = now;
            post.UpdatedAt = now;

            blogDataContext.BlogPosts.Add(post);
            await blogDataContext.SaveChangesAsync();

            logger.LogInformation("Blog post with ID {BlogPostId} added", post.Id);

            return post.Id;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error adding blog post");
            throw;
        }
    }

    public async Task<bool> UpdateAsync(Guid blogId, AddOrUpdateBlogPostCommand command)
    {
        try
        {
            logger.LogInformation("Updating blog post with ID {BlogPostId}", command.Id);

            var existingPost = await blogDataContext.BlogPosts
                .Where(x => x.BlogId == blogId && x.Id == command.Id)
                .FirstOrDefaultAsync();

            if (existingPost == null)
            {
                logger.LogWarning("Blog post with ID {BlogPostId} not found", command.Id);
                return false;
            }

            command.MapToExistingBlogPost(existingPost);

            existingPost.UpdatedAt = DateTimeOffset.UtcNow;

            await blogDataContext.SaveChangesAsync();

            logger.LogInformation("Blog post with ID {BlogPostId} updated", command.Id);

            return true;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error updating blog post with ID {BlogPostId}", command.Id);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(Guid blogId, Guid id)
    {
        try
        {
            logger.LogInformation("Deleting blog post with ID {BlogPostId}", id);

            var existingPost = await blogDataContext.BlogPosts
                .Where(x => x.BlogId == blogId && x.Id == id)
                .FirstOrDefaultAsync();

            if (existingPost == null)
            {
                logger.LogWarning("Blog post with ID {BlogPostId} not found", id);
                return false;
            }

            blogDataContext.BlogPosts.Remove(existingPost);
            await blogDataContext.SaveChangesAsync();

            logger.LogInformation("Blog post with ID {BlogPostId} deleted", id);

            return true;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error deleting blog post with ID {BlogPostId}", id);
            return false;
        }
    }
}