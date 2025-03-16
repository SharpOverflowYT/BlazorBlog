using BlazorBlog.Server.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlazorBlog.Server.Persistence;

public class BlogDataContext(DbContextOptions<BlogDataContext> options) : DbContext(options)
{
    public DbSet<Blog> Blogs { get; set; }

    public DbSet<BlogPost> BlogPosts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}