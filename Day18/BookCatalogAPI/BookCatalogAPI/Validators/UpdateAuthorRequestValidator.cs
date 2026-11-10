using BookCatalogAPI.DTOs;
using FluentValidation;

namespace BookCatalogAPI.Validators
{
    public class UpdateAuthorRequestValidator : AbstractValidator<UpdateAuthorRequest>
    {
        public UpdateAuthorRequestValidator()
        {
            RuleFor(x => x.author)
                .NotEmpty().WithMessage("Author is required.")
                .MinimumLength(6).WithMessage("Author must be at least 6 characters long.")
                .MaximumLength(50).WithMessage("Author must not exceed 50 characters.");
        }
    }
}
