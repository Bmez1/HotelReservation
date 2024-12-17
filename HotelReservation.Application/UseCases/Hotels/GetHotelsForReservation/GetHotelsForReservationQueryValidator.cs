using FluentValidation;

namespace HotelReservation.Application.UseCases.Hotels.GetHotelsForReservation;

public class GetHotelsForReservationQueryValidator : AbstractValidator<GetHotelsForReservationQuery>
{
    public GetHotelsForReservationQueryValidator()
    {
        RuleFor(x => x.CheckInDate)
            .NotEmpty()
            .Must(BeAFutureDate)
            .WithMessage("Check-in date must be today or in the future.");

        RuleFor(x => x.CheckOutDate)
            .NotEmpty()
            .Must(BeAFutureDate)
            .WithMessage("Check-out date must be today or in the future.");

        RuleFor(x => x)
            .Must(HaveValidDateRange)
            .WithMessage("Check-in date must be earlier than the check-out date.");

        RuleFor(x => x.NumberOfGuests)
            .NotEmpty()
            .WithMessage("Number of guests is required.")
            .GreaterThan(0)
            .WithMessage("Number of guests must be greater than 0.");

        RuleFor(x => x.DestinationCity).NotEmpty();
    }

    private static bool BeAFutureDate(DateTime date)
    {
        return date.Date >= DateTime.Now.Date;
    }

    private static bool HaveValidDateRange(GetHotelsForReservationQuery query)
    {
        return query.CheckInDate < query.CheckOutDate;
    }
}


