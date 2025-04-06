using BlazorBlog.Shared.Blogs.Commands;
using FastEndpoints;

namespace BlazorBlog.Server.Slices.Blogs.Commands;

public class UpdateBlogHandler(IBlogService blogService) : Endpoint<AddOrUpdateBlogCommand>
{
    public override void Configure()
    {
        Put(Shared.Constants.Routes.Api.Blogs.Update);
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddOrUpdateBlogCommand command, CancellationToken cancellationToken)
    {
        var isSuccessful = await blogService.UpdateAsync(command);

        if (!isSuccessful)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        await SendOkAsync(cancellationToken);
    }
}