using Common;

using MediatR;

namespace HotelReservation.Application.UseCases.Rooms.ChangeStateRoom
{
    public record ChangeStateRoomCommand(
        Guid HotelId,
        Guid RoomId,
        bool Enable,
        string? ReasonDisable) : IRequest<Result<Guid>>;
}
