using BlazorBlog.Server.Slices.BlogPosts.Queries;
using BlazorBlog.Shared.Blogs.Commands;
using FastEndpoints;

namespace BlazorBlog.Server.Slices.BlogPosts.Commands;

public class AddBlogPostHandler(IBlogPostService blogPostService) : Endpoint<AddOrUpdateBlogPostCommand>
{
    public override void Configure()
    {
        Post(Shared.Constants.Routes.Api.BlogPosts.Add);
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddOrUpdateBlogPostCommand command, CancellationToken cancellationToken)
    {
        var blogId = Route<Guid>("blogId");

        var id = await blogPostService.AddAsync(blogId, command);

        await SendCreatedAtAsync<GetBlogPostByIdHandler>(new { blogId, id }, id, cancellation: cancellationToken);
    }
}