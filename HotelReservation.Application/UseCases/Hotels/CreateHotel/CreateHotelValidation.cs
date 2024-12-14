using FluentValidation;

namespace HotelReservation.Application.UseCases.Hotels.CreateHotel;

internal sealed class UpdateHotelValidation : AbstractValidator<CreateHotelCommand>
{
    public UpdateHotelValidation()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Country).NotEmpty();
        RuleFor(x => x.City).NotEmpty();
        RuleFor(x => x.Phone).NotEmpty().GreaterThan(0);
    }
}
