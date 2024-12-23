using FluentValidation;

using HotelReservation.Domain.Enums;

namespace HotelReservation.Application.UseCases.Reservations.CreateReservation
{
    public class CreatePassengerValidator : AbstractValidator<CreatePassengerCommand>
    {
        public CreatePassengerValidator()
        {
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.Gender).NotEmpty()
                .Must(value => Enum.IsDefined(typeof(Gender), value))
                .WithMessage("Invalid gender.");
            RuleFor(x => x.DocumentType).NotEmpty()
                .Must(value => Enum.IsDefined(typeof(DocumentType), value))
                .WithMessage("Invalid document type.");
            RuleFor(x => x.DocumentNumber).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.PhoneNumber).NotEmpty();
        }
    }
}
