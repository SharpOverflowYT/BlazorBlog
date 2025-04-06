using BlazorBlog.Shared.Blogs;
using FastEndpoints;

namespace BlazorBlog.Server.Slices.BlogPosts.Queries;

public class GetAllBlogPostsHandler(IBlogPostService blogPostService) : EndpointWithoutRequest<List<BlogPostDto>>
{
    public override void Configure()
    {
        Get(Shared.Constants.Routes.Api.BlogPosts.GetAll);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {
        var blogId = Route<Guid>("blogId");

        var posts = await blogPostService.GetAllAsync(blogId);

        await SendOkAsync(posts, cancellationToken);
    }
}