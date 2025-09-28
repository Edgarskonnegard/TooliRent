using FluentValidation;
using TooliRent.Application.DTOs.Category;

namespace TooliRent.Application.Validators
{
    public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required")
                .MaximumLength(50).WithMessage("Category name cannot exceed 50 characters");
        }
    }
}
