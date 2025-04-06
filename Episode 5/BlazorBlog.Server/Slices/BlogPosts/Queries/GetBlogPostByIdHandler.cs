using BlazorBlog.Shared.Blogs;
using FastEndpoints;

namespace BlazorBlog.Server.Slices.BlogPosts.Queries;

public class GetBlogPostByIdHandler(IBlogPostService blogPostService) : EndpointWithoutRequest<BlogPostDto?>
{
    public override void Configure()
    {
        Get(Shared.Constants.Routes.Api.BlogPosts.GetById);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var blogId = Route<Guid>("blogId");
        var id = Route<Guid>("id");

        var post = await blogPostService.GetByIdAsync(blogId, id);

        if (post == null)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        await SendAsync(post, cancellation: cancellationToken);
    }
}