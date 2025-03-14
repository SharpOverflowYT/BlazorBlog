﻿using System.Text.RegularExpressions;

namespace BlazorBlog.Shared.Constants;

public static partial class Routes
{
    [GeneratedRegex("{.*?}")]
    private static partial Regex StringFormatArgsRegex();

    public static string Format(this string template, params object[] args)
    {
        var index = 0;
        var formattedTemplate = StringFormatArgsRegex().Replace(template, _ => $"{{{index++}}}");
        return string.Format(formattedTemplate, args);
    }
    
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
            public const string Add = "/blogs/add";
            public const string Edit = "/blogs/edit/{id:guid}";
        }
    }
}