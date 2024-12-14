using FluentValidation;

namespace HotelReservation.Application.UseCases.Hotels.UpdateHotel;

internal sealed class UpdateHotelValidation : AbstractValidator<UpdateHotelCommand>
{
    public UpdateHotelValidation()
    {
        RuleFor(x => x.HotelId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Country).NotEmpty();
        RuleFor(x => x.City).NotEmpty();
        RuleFor(x => x.Phone).NotEmpty().GreaterThan(0);
    }
}
