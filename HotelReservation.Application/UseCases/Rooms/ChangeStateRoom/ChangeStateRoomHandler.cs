using Common;

using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Errors;

using MediatR;

namespace HotelReservation.Application.UseCases.Rooms.ChangeStateRoom
{
    public class ChangeStateRoomHandler(IHotelRepository hotelRepository) : IRequestHandler<ChangeStateRoomCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(ChangeStateRoomCommand request, CancellationToken cancellationToken)
        {
            var hotel = (await hotelRepository.ListAsync(h => h.Id == request.HotelId &&
                h.Rooms.Any(r => r.Id == request.RoomId), true, ["Rooms"]))
                .FirstOrDefault();

            var room = hotel?.Rooms.FirstOrDefault(r => r.Id == request.RoomId);

            if (hotel is null || !hotel.IsEnabled)
            {
                return Result.Failure<Guid>(HotelError.NotFoundByIdOrDisabled);
            }

            if (room is null)
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

            await hotelRepository.SaveChangesAsync();

            return Result.Success(room.Id);
        }
    }
}
