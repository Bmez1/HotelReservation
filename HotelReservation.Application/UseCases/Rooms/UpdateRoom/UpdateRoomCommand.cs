using Common;

using HotelReservation.Domain.Enums;

using MediatR;

namespace HotelReservation.Application.UseCases.Rooms.UpdateRoom
{
    public record UpdateRoomCommand(
    Guid HotelId,
    Guid RoomId,
    string RoomNumber,
    decimal BaseCost,
    decimal Taxes,
    RoomType Type,
    int BedCount,
    int Capacity,
    string Location) : IRequest<Result<Guid>>;
}
