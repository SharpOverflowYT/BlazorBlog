using BlazorBlog.Shared.Blogs;
using BlazorBlog.Shared.Blogs.Commands;
using Riok.Mapperly.Abstractions;

namespace BlazorBlog.Client.Extensions;

[Mapper]
public static partial class MapperExtensions
{
    public static partial AddOrUpdateBlogCommand MapToCommand(this BlogDto blog);

    public static partial AddOrUpdateBlogPostCommand MapToCommand(this BlogPostDto post);
}