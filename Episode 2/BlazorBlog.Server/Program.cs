using BlazorBlog.Server.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddDbContext<BlogDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogDataContext"));
});

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

app.MapControllers();

app.Run();