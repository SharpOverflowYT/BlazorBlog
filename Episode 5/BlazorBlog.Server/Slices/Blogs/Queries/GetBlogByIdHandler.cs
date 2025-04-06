using BlazorBlog.Shared.Blogs;
using FastEndpoints;

namespace BlazorBlog.Server.Slices.Blogs.Queries;

public class GetBlogByIdHandler(IBlogService blogService) : EndpointWithoutRequest<BlogDto?>
{
    public override void Configure()
    {
        Get(Shared.Constants.Routes.Api.Blogs.GetById);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var id = Route<Guid>("id");

        var blog = await blogService.GetByIdAsync(id);

        if (blog == null)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        await SendOkAsync(blog, cancellationToken);
    }
}