using BlazorBlog.Shared.Blogs.Commands;
using BlazorBlog.Shared.Constants;
using FluentValidation;

namespace BlazorBlog.Shared.Blogs.Validators;

public class AddOrUpdateBlogValidator : AbstractValidator<AddOrUpdateBlogCommand>
{
    public AddOrUpdateBlogValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("The title is required")
            .MaximumLength(MaxLengths.Blogs.Title)
            .WithMessage("The title must be less than {MaxLength} characters");
        
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("The description is required")
            .MaximumLength(MaxLengths.Blogs.Description)
            .WithMessage("The description must be less than {MaxLength} characters");
    }
}