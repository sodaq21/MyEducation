using BookCatalogAPI.DTOs;
using FluentValidation;

namespace BookCatalogAPI.Validators
{
    public class UpdateYearRequestValidator : AbstractValidator<UpdateYearRequest>
    {
        public UpdateYearRequestValidator()
        {
            RuleFor(x => x.Year)
                .NotEmpty().WithMessage("Year is required.")
                .InclusiveBetween(1, DateTime.UtcNow.Year).WithMessage("An invalid year was entered.");
        }
    }
}
