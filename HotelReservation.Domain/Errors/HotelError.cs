using Common;

namespace HotelReservation.Domain.Errors
{
    public static class HotelError
    {
        public static Error AlreadyExists => Error.Conflict(
        "Hotel.Exists",
        "There is a hotel in the same country and city with the same name");

        public static Error NotFoundById => Error.NotFound(
            "Hotel.NotFound",
            "There is no hotel with the given id");
    }
}
