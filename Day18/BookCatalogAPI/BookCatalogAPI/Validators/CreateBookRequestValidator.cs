using BookCatalogAPI.DTOs;
using FluentValidation;

namespace BookCatalogAPI.Validators
{
    public class CreateBookRequestValidator : AbstractValidator<CreateBookRequest>
    {
        public CreateBookRequestValidator()
        {
            RuleFor(x => x.title)
                .NotEmpty().WithMessage("Title is required.")
                .MinimumLength(4).WithMessage("Title must be at least 4 characters long.")
                .MaximumLength(50).WithMessage("Title must not exceed 50 characters.");
                
            RuleFor(x => x.author)
                .MaximumLength(50).WithMessage("Title must not exceed 50 characters.");

            RuleFor(x => x.year)
                .NotEmpty().WithMessage("Year is required.")
                .InclusiveBetween(1, DateTime.Now.Year).WithMessage("An invalid year was entered.");
        }
    }
}
