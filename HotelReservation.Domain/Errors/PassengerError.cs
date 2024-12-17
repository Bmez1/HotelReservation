using Common;

namespace HotelReservation.Domain.Errors
{
    public static class PassengerError
    {
        public static Error NotFoundById => Error.NotFound(
            "Passenger.NotFound",
            "There is no passenger with the given id");
    }
}
