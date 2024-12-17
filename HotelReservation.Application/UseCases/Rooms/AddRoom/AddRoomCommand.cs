using Common;

using HotelReservation.Application.UseCases.Rooms.Dtos;
using HotelReservation.Domain.Enums;

using MediatR;

namespace HotelReservation.Application.UseCases.Rooms.AddRoom
{
    public record AddRoomCommand(
        Guid HotelId,
        string RoomNumber,
        decimal BaseCost,
        decimal Taxes,
        RoomType Type,
        int BedCount,
        int Capacity,
        string Location) : IRequest<Result<RoomResponseDto>>;
}
