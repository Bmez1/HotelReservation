using Common;

using HotelReservation.Application.UseCases.Rooms.Dtos;

using MediatR;

namespace HotelReservation.Application.UseCases.Rooms.GetRooms
{
    public record GetRoomsQuery(Guid HotelId, bool All) : IRequest<Result<IEnumerable<RoomResponseDto>>>;
}
