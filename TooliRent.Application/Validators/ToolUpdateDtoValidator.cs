using FluentValidation;
using TooliRent.Application.DTOs.Tool;

namespace TooliRent.Application.Validators.Tool
{
    public class ToolUpdateDtoValidator : AbstractValidator<ToolUpdateDto>
    {
        public ToolUpdateDtoValidator()
        {
            RuleFor(t => t.Name)
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters")
                .When(t => !string.IsNullOrWhiteSpace(t.Name));

            RuleFor(t => t.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters")
                .When(t => !string.IsNullOrWhiteSpace(t.Description));

            RuleFor(t => t.PricePerDay)
                .GreaterThan(0).WithMessage("Price per day must be greater than 0")
                .When(t => t.PricePerDay != null);

            RuleFor(t => t.CategoryId)
                .GreaterThan(0).WithMessage("CategoryId must be a valid value")
                .When(t => t.CategoryId != null);
        }
    }
}
