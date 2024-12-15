using Common;

namespace HotelReservation.Domain.Errors
{
    public static class RoomError
    {
        public static Error AlreadyExists => Error.Conflict(
        "Room.Exists",
        "The hotel already has a room with the same number.");

        public static Error NotFound => Error.NotFound(
        "Room.NotFound",
        "There is no room with the given id");

        public static Error ReasonDisableEmpty => Error.Failure(
        "Room.ReasonDisableEmpty",
        "You must provide a reason to disable the room.");
    }
}
