using FluentValidation;
using TooliRent.Application.DTOs.Tool;

namespace TooliRent.Application.Validators.Tool
{
    public class ToolCreateDtoValidator : AbstractValidator<ToolCreateDto>
    {
        public ToolCreateDtoValidator()
        {
            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(t => t.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters");

            RuleFor(t => t.PricePerDay)
                .GreaterThan(0).WithMessage("Price per day must be greater than 0");

            RuleFor(t => t.CategoryId)
                .GreaterThan(0).WithMessage("CategoryId must be a valid value");

       }
    }
}
