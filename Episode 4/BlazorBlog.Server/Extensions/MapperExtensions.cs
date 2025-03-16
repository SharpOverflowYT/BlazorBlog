using BlazorBlog.Server.Domain;
using BlazorBlog.Shared.Blogs;
using BlazorBlog.Shared.Blogs.Commands;
using Riok.Mapperly.Abstractions;

namespace BlazorBlog.Server.Extensions;

[Mapper]
public static partial class MapperExtensions
{
    public static partial IQueryable<BlogDto> ProjectToDtos(this IQueryable<Blog> queryable);

    public static partial IQueryable<BlogPostDto> ProjectToDtos(this IQueryable<BlogPost> queryable);

    public static partial Blog MapToNewBlog(this AddOrUpdateBlogCommand command);

    public static partial BlogPost MapToNewBlogPost(this AddOrUpdateBlogPostCommand command);

    public static partial void MapToExistingBlog(this AddOrUpdateBlogCommand command, Blog existingBlog);

    public static partial void MapToExistingBlogPost(this AddOrUpdateBlogPostCommand command, BlogPost existingBlogPost);
}