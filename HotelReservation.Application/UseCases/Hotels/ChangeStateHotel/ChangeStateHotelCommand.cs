using Common;

using MediatR;

namespace HotelReservation.Application.UseCases.Hotels.ChangeStateHotel
{
    public record ChangeStateHotelCommand(Guid HotelId, bool Enable) : IRequest<Result<Guid>>;
}
