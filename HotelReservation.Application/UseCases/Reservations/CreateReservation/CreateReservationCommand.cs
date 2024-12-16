using Common;

using MediatR;

namespace HotelReservation.Application.UseCases.Reservations.CreateReservation
{
    public record CreateReservationCommand() : IRequest<Result<string>>;
}
