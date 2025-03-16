using BlazorBlog.Server.Extensions;
using BlazorBlog.Server.Persistence;
using BlazorBlog.Shared.Blogs;
using BlazorBlog.Shared.Blogs.Commands;
using BlazorBlog.Shared.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorBlog.Server.Controllers;

[ApiController]
public class BlogsPostsController(BlogDataContext blogDataContext) : Controller
{
    [HttpGet(Routes.Api.BlogPosts.GetAll)]
    public async Task<ActionResult<List<BlogPostDto>>> GetAll(Guid blogId)
    {
        var posts = await blogDataContext.BlogPosts
            .Where(x => x.BlogId == blogId)
            .ProjectToDtos()
            .ToListAsync();

        return Ok(posts);
    }

    [HttpGet(Routes.Api.BlogPosts.GetById)]
    public async Task<ActionResult<BlogPostDto?>> GetById(Guid blogId, Guid id)
    {
        var post = await blogDataContext.BlogPosts
            .Where(x => x.BlogId == blogId && x.Id == id)
            .ProjectToDtos()
            .FirstOrDefaultAsync();

        if (post == null)
            return NotFound();

        return Ok(post);
    }

    [HttpPost(Routes.Api.BlogPosts.Add)]
    public async Task<ActionResult> Add(Guid blogId, AddOrUpdateBlogPostCommand command)
    {
        var post = command.MapToNewBlogPost();

        post.Id = Guid.NewGuid();
        post.BlogId = blogId;

        var now = DateTimeOffset.UtcNow;

        post.CreatedAt = now;
        post.UpdatedAt = now;

        blogDataContext.BlogPosts.Add(post);
        await blogDataContext.SaveChangesAsync();

        return Created();
    }

    [HttpPut(Routes.Api.BlogPosts.Update)]
    public async Task<ActionResult> Update(Guid blogId, AddOrUpdateBlogPostCommand command)
    {
        var existingPost = await blogDataContext.BlogPosts
            .Where(x => x.BlogId == blogId && x.Id == command.Id)
            .FirstOrDefaultAsync();

        if (existingPost == null)
            return NotFound();

        command.MapToExistingBlogPost(existingPost);

        existingPost.UpdatedAt = DateTimeOffset.UtcNow;

        await blogDataContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete(Routes.Api.BlogPosts.Delete)]
    public async Task<ActionResult> Delete(Guid blogId, Guid id)
    {
        var existingPost = await blogDataContext.BlogPosts
            .Where(x => x.BlogId == blogId && x.Id == id)
            .FirstOrDefaultAsync();

        if (existingPost == null)
            return NotFound();

        blogDataContext.BlogPosts.Remove(existingPost);
        await blogDataContext.SaveChangesAsync();

        return Ok();
    }
}