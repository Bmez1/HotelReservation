using Common;

using HotelReservation.Application.UseCases.Hotels.Dtos;

using MediatR;

namespace HotelReservation.Application.UseCases.Hotels.ChangeStateHotel
{
    public record ChangeStateHotelCommand(Guid HotelId, bool Enable) : IRequest<Result<Guid>>;
}
