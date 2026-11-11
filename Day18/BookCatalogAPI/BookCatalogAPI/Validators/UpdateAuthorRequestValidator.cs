using BookCatalogAPI.DTOs;
using FluentValidation;

namespace BookCatalogAPI.Validators
{
    public class UpdateAuthorRequestValidator : AbstractValidator<UpdateAuthorRequest>
    {
        public UpdateAuthorRequestValidator()
        {
            RuleFor(x => x.Author)
                .MaximumLength(50).WithMessage("Author must not exceed 50 characters.");
        }
    }
}
