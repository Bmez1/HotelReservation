using Common;

using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Errors;

using MediatR;

namespace HotelReservation.Application.UseCases.Rooms.ChangeStateRoom
{
    public class ChangeStateRoomHandler(IRoomRepository roomRepository) : IRequestHandler<ChangeStateRoomCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(ChangeStateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await roomRepository.GetByIdAsync(request.RoomId, true);
            if (room is null || room.HotelId != request.HotelId)
            {
                return Result.Failure<Guid>(RoomError.NotFound);
            }

            if (request.Enable)
            {
                room.Activate();
            }
            else
            {

                if (string.IsNullOrEmpty(request.ReasonDisable))
                {
                    return Result.Failure<Guid>(RoomError.ReasonDisableEmpty);
                }

                room.Deactivate(request.ReasonDisable!);
            }

            await roomRepository.SaveChangesAsync();

            return Result.Success(room.Id);
        }
    }
}
