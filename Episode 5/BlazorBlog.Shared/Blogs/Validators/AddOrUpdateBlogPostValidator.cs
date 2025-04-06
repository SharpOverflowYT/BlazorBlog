using BlazorBlog.Shared.Blogs.Commands;
using BlazorBlog.Shared.Constants;
using FluentValidation;

namespace BlazorBlog.Shared.Blogs.Validators;

public class AddOrUpdateBlogPostValidator : AbstractValidator<AddOrUpdateBlogPostCommand>
{
    public AddOrUpdateBlogPostValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("The title is required")
            .MaximumLength(MaxLengths.BlogPosts.Title)
            .WithMessage("The title must be less than {MaxLength} characters");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("The content is required")
            .MaximumLength(MaxLengths.BlogPosts.Content)
            .WithMessage("The content must be less than {MaxLength} characters");
    }
}