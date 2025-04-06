using BlazorBlog.Server.Persistence;
using BlazorBlog.Server.Slices.BlogPosts;
using BlazorBlog.Server.Slices.Blogs;
using BlazorBlog.Shared.Blogs.Validators;
using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddFastEndpoints(options =>
{
    options.IncludeAbstractValidators = true;
});

builder.Services.AddDbContext<BlogDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogDataContext"));
});

builder.Services.AddValidatorsFromAssemblyContaining<AddOrUpdateBlogValidator>();

builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IBlogPostService, BlogPostService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseWebAssemblyDebugging();
}
else
{
   app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseBlazorFrameworkFiles();
app.MapFallbackToFile("index.html");

app.UseFastEndpoints();

app.Run();