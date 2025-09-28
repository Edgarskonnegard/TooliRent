using FluentValidation;
using TooliRent.Application.DTOs.User;
using System.Text.RegularExpressions;

namespace TooliRent.Application.Validators.User
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidator()
        {
            RuleFor(u => u.Username)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email must be valid")
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                .WithMessage("Email must be in a valid format (example: name@domain.com)");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .Matches(@"[A-Z]+").WithMessage("Password must contain at least one uppercase letter")
                .Matches(@"\d+").WithMessage("Password must contain at least one number")
                .Matches(@"[\!\?\*\.\@\$\%\^\&\#]+").WithMessage("Password must contain at least one special character (!?*.@$%^&#)");
        }
    }
}
