using FluentValidation;

namespace HotelReservation.Application.UseCases.Reservations.CreateReservation
{
    public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
    {
        public CreateReservationCommandValidator()
        {
            RuleFor(x => x.HotelId).NotEmpty();
            RuleFor(x => x.RoomId).NotEmpty();
            RuleFor(x => x.TravelerId).NotEmpty();
            RuleFor(x => x.EmergencyContactFullName).NotEmpty();
            RuleFor(x => x.EmergencyContactPhoneNumber).NotEmpty();

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
        }

        private static bool BeAFutureDate(DateTime date)
        {
            return date.Date >= DateTime.Now.Date;
        }

        private static bool HaveValidDateRange(CreateReservationCommand query)
        {
            return query.CheckInDate < query.CheckOutDate;
        }
    }
}
