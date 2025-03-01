using BlazorBlog.Server.Domain;
using BlazorBlog.Server.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorBlog.Server.Controllers;

[ApiController, Route("api/blogs")]
public class BlogsController(BlogDataContext blogDataContext) : Controller
{
    [HttpGet("")]
    public async Task<ActionResult<List<Blog>>> GetAll()
    {
        var blogs = await blogDataContext.Blogs.ToListAsync();
        return Ok(blogs);
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<Blog?>> GetById(Guid id)
    {
        var blog = await blogDataContext.Blogs.FindAsync(id);

        if (blog == null)
            return NotFound();

        return Ok(blog);
    }

    [HttpPost("")]
    public async Task<ActionResult> Add(Blog blog)
    {
        blog.Id = Guid.NewGuid();
        
        blogDataContext.Blogs.Add(blog);
        await blogDataContext.SaveChangesAsync();

        return Created();
    }

    [HttpPut("")]
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

    [HttpDelete("{id:Guid}")]
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