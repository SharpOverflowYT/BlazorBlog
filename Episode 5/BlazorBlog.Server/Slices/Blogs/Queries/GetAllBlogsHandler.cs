using BlazorBlog.Shared.Blogs;
using FastEndpoints;

namespace BlazorBlog.Server.Slices.Blogs.Queries;

public class GetAllBlogsHandler(IBlogService blogService) : EndpointWithoutRequest<List<BlogDto>>
{
    public override void Configure()
    {
        Get(Shared.Constants.Routes.Api.Blogs.GetAll);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var blogs = await blogService.GetAllAsync();
        await SendOkAsync(blogs, cancellationToken);
    }
}