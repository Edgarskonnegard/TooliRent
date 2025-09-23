using FluentValidation;
using TooliRent.Application.DTOs.Booking;

public class BookingCreateDtoValidator : AbstractValidator<BookingCreateDto>
{
    public BookingCreateDtoValidator()
    {
        RuleFor(b => b.UserId)
            .GreaterThan(0).WithMessage("UserId must be greater than 0");

        RuleFor(b => b.ToolId)
            .GreaterThan(0).WithMessage("ToolId must be greater than 0");

        RuleFor(b => b.StartDate)
            .LessThan(b => b.EndDate).WithMessage("StartDate must be before EndDate");

        RuleFor(b => b.EndDate)
            .GreaterThan(DateTime.UtcNow).WithMessage("EndDate must be in the future");
    }
}
