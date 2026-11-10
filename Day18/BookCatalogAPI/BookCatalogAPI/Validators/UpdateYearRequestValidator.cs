using BookCatalogAPI.DTOs;
using FluentValidation;

namespace BookCatalogAPI.Validators
{
    public class UpdateYearRequestValidator : AbstractValidator<UpdateYearRequest>
    {
        public UpdateYearRequestValidator()
        {
            RuleFor(x => x.year)
                .NotEmpty().WithMessage("Year is required.")
                .InclusiveBetween(1, DateTime.Now.Year).WithMessage("An invalid year was entered.");
        }
    }
}
