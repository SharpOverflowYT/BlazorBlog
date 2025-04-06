using FastEndpoints;

namespace BlazorBlog.Server.Slices.BlogPosts.Commands;

public class DeleteBlogPostHandler(IBlogPostService blogPostService) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete(Shared.Constants.Routes.Api.BlogPosts.Delete);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var blogId = Route<Guid>("blogId");
        var id = Route<Guid>("id");

        var isSuccessful = await blogPostService.DeleteAsync(blogId, id);

        if (!isSuccessful)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        await SendOkAsync(cancellationToken);
    }
}