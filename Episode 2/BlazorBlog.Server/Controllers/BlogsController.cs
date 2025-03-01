using BlazorBlog.Server.Domain;
using BlazorBlog.Server.Extensions;
using BlazorBlog.Server.Persistence;
using BlazorBlog.Shared.Blogs;
using BlazorBlog.Shared.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorBlog.Server.Controllers;

[ApiController]
public class BlogsController(BlogDataContext blogDataContext) : Controller
{
    [HttpGet(Routes.Api.Blogs.GetAll)]
    public async Task<ActionResult<List<BlogDto>>> GetAll()
    {
        var blogs = await blogDataContext.Blogs
            .ProjectToDtos()
            .ToListAsync();
        
        return Ok(blogs);
    }

    [HttpGet(Routes.Api.Blogs.GetById)]
    public async Task<ActionResult<BlogDto?>> GetById(Guid id)
    {
        var blog = await blogDataContext.Blogs
            .ProjectToDtos()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (blog == null)
            return NotFound();

        return Ok(blog);
    }

    [HttpPost(Routes.Api.Blogs.Add)]
    public async Task<ActionResult> Add(Blog blog)
    {
        blog.Id = Guid.NewGuid();
        
        blogDataContext.Blogs.Add(blog);
        await blogDataContext.SaveChangesAsync();

        return Created();
    }

    [HttpPut(Routes.Api.Blogs.Update)]
    public async Task<ActionResult> Update(Blog updatedBlog)
    {
        var existingBlog = await blogDataContext.Blogs.FindAsync(updatedBlog.Id);

        if (existingBlog == null)
            return NotFound();

        existingBlog.Title = updatedBlog.Title;
        existingBlog.Description = updatedBlog.Description;

        await blogDataContext.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete(Routes.Api.Blogs.Delete)]
    public async Task<ActionResult> Delete(Guid id)
    {
        var existingBlog = await blogDataContext.Blogs.FindAsync(id);

        if (existingBlog == null)
            return NotFound();

        blogDataContext.Blogs.Remove(existingBlog);
        await blogDataContext.SaveChangesAsync();

        return Ok();
    }
}