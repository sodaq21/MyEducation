using BookCatalogAPI.DTOs;
using FluentValidation;

namespace BookCatalogAPI.Validators
{
    public class CreateBookRequestValidator : AbstractValidator<CreateBookRequest>
    {
        public CreateBookRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(50).WithMessage("Title must not exceed 50 characters.");
                
            RuleFor(x => x.Author)
                .MaximumLength(50).WithMessage("Author must not exceed 50 characters.");

            RuleFor(x => x.Year)
                .NotEmpty().WithMessage("Year is required.")
                .InclusiveBetween(1, DateTime.UtcNow.Year).WithMessage("An invalid year was entered.");
        }
    }
}
