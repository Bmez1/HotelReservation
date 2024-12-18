using Common;

using HotelReservation.Application.Interfaces;
using HotelReservation.Application.UseCases.Rooms.Dtos;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Errors;

using MediatR;

namespace HotelReservation.Application.UseCases.Rooms.GetRooms
{
    public class GetRoomsHandler(IHotelRepository hotelRepository) : IRequestHandler<GetRoomsQuery, Result<IEnumerable<RoomResponseDto>>>
    {
        public async Task<Result<IEnumerable<RoomResponseDto>>> Handle(GetRoomsQuery request, CancellationToken cancellationToken)
        {
            Hotel? hotel = await hotelRepository.GetByIdAsync(request.HotelId, navigationProperties: ["Rooms"]);

            if (hotel is null)
            {
                return Result.Failure<IEnumerable<RoomResponseDto>>(HotelError.NotFoundById);
            }

            var rooms = hotel.GetRooms(request.All).Select(room => new RoomResponseDto()
            {
                Id = room.Id,
                RoomNumber = room.Number,
                BaseCost = room.BaseCost,
                HotelId = hotel.Id,
                Location = room.Location,
                Taxes = room.Taxes,
                Type = room.Type.ToString()
            });

            return Result.Success(rooms, totalData: rooms.Count());
        }
    }
}
