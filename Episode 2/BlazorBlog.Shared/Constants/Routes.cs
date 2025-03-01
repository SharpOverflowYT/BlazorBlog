namespace BlazorBlog.Shared.Constants;

public static class Routes
{
    public static class Api
    {
        public static class Blogs
        {
            public const string GetAll = "api/blogs";
            public const string GetById = "api/blogs/{id:Guid}";
            public const string Add = "api/blogs";
            public const string Update = "api/blogs";
            public const string Delete = "api/blogs/{id:Guid}";
        }
    }

    public static class Pages
    {
        public const string Home = "/";

        public static class Blogs
        {
            public const string Index = "/blogs";
        }
    }
}