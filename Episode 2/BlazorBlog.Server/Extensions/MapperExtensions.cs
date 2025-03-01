using BlazorBlog.Server.Domain;
using BlazorBlog.Shared.Blogs;
using Riok.Mapperly.Abstractions;

namespace BlazorBlog.Server.Extensions;

[Mapper]
public static partial class MapperExtensions
{
    public static partial IQueryable<BlogDto> ProjectToDtos(this IQueryable<Blog> queryable);
}