using Common;

using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Errors;

using MediatR;

namespace HotelReservation.Application.UseCases.Rooms.UpdateRoom
{
    public class UpdateRoomHandler(IRoomRepository roomRepository) : IRequestHandler<UpdateRoomCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await roomRepository.GetByIdAsync(request.RoomId, true);
            if (room is null || room.HotelId != request.HotelId)
            {
                return Result.Failure<Guid>(RoomError.NotFound);
            }

            if (room.Number != request.RoomNumber && await roomRepository.ExistsAsync(x => x.Number == request.RoomNumber && x.HotelId == request.HotelId))
            {
                return Result.Failure<Guid>(RoomError.AlreadyExists);
            }

            room.Update(request.BaseCost, request.Taxes, request.Type, request.RoomNumber, request.Location, request.BedCount, request.Capacity);

            await roomRepository.SaveChangesAsync();

            return Result.Success(room.Id);
        }
    }
}
