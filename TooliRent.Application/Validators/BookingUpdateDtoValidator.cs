using FluentValidation;
using TooliRent.Application.DTOs.Booking;

namespace TooliRent.Application.Validators.Booking
{
    public class BookingUpdateDtoValidator : AbstractValidator<BookingUpdateDto>
    {
        public BookingUpdateDtoValidator()
        {
            RuleFor(b => b.StartDate)
                .LessThan(b => b.EndDate)
                .WithMessage("StartDate must be before EndDate");

            RuleFor(b => b.EndDate)
                .GreaterThan(b => b.StartDate)
                .WithMessage("EndDate must be after StartDate");
        }
    }
}
