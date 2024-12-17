using Common;

using HotelReservation.Application.Interfaces;
using HotelReservation.Application.UseCases.Rooms.Dtos;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Errors;

using MediatR;

namespace HotelReservation.Application.UseCases.Rooms.AddRoom
{
    public class AddRoomHandler(
        IHotelRepository hotelRepository,
        IRoomRepository roomRepository) : IRequestHandler<AddRoomCommand, Result<RoomResponseDto>>
    {
        public async Task<Result<RoomResponseDto>> Handle(AddRoomCommand request, CancellationToken cancellationToken)
        {
            Hotel? hotel = await hotelRepository.GetByIdAsync(request.HotelId);

            if (hotel is null)
            {
                return Result.Failure<RoomResponseDto>(HotelError.NotFoundById);
            }

            if (await roomRepository.ExistsAsync(x => x.Number == request.RoomNumber && x.HotelId == request.HotelId))
            {
                return Result.Failure<RoomResponseDto>(RoomError.AlreadyExists);
            }

            Room room = Room.Create(
                request.RoomNumber,
                request.BaseCost,
                request.Taxes,
                request.Location,
                request.Type,
                hotel.Id,
                request.BedCount,
                request.Capacity);

            await roomRepository.AddAsync(room);
            await roomRepository.SaveChangesAsync();

            return new RoomResponseDto()
            {
                HotelId = room.HotelId,
                Id = room.Id,
                RoomNumber = room.Number,
                BaseCost = room.BaseCost,
                Taxes = room.Taxes,
                Type = nameof(room.Type),
                Location = room.Location
            };
        }
    }
}
