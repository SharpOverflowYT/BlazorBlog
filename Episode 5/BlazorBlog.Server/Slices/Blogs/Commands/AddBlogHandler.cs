using BlazorBlog.Server.Slices.Blogs.Queries;
using BlazorBlog.Shared.Blogs.Commands;
using FastEndpoints;

namespace BlazorBlog.Server.Slices.Blogs.Commands;

public class AddBlogHandler(IBlogService blogService) : Endpoint<AddOrUpdateBlogCommand>
{
    public override void Configure()
    {
        Post(Shared.Constants.Routes.Api.Blogs.Add);
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddOrUpdateBlogCommand command, CancellationToken cancellationToken)
    {
        var id = await blogService.AddAsync(command);
        await SendCreatedAtAsync<GetBlogByIdHandler>(id, id, cancellation: cancellationToken);
    }
}