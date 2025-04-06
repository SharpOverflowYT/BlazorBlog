using BlazorBlog.Shared.Blogs.Commands;
using FastEndpoints;

namespace BlazorBlog.Server.Slices.BlogPosts.Commands;

public class UpdateBlogPostHandler(IBlogPostService blogPostService) : Endpoint<AddOrUpdateBlogPostCommand>
{
    public override void Configure()
    {
        Put(Shared.Constants.Routes.Api.BlogPosts.Update);
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddOrUpdateBlogPostCommand command, CancellationToken cancellationToken)
    {
        var blogId = Route<Guid>("blogId");

        var isSuccessful = await blogPostService.UpdateAsync(blogId, command);

        if (!isSuccessful)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        await SendOkAsync(cancellationToken);
    }
}