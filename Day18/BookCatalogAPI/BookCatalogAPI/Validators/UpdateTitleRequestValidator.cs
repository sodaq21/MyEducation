using BookCatalogAPI.DTOs;
using FluentValidation;

namespace BookCatalogAPI.Validators
{
    public class UpdateTitleRequestValidator : AbstractValidator<UpdateTitleRequest>
    {
        public UpdateTitleRequestValidator()
        {
            RuleFor(x => x.title)
                .NotEmpty().WithMessage("Title is required.")
                .MinimumLength(4).WithMessage("Title must be at least 4 characters long.")
                .MaximumLength(50).WithMessage("Title must not exceed 50 characters");
                
        }
    }
}
