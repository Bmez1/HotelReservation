using Common;

namespace HotelReservation.Domain.Errors
{
    public static class ReservationError
    {
        public static Error NotFoundById => Error.NotFound(
            "Reservation.NotFound",
       "There is no reservation with the given id");

        public static Error CannotAddPassenger => Error.Validation(
            "Reservation.CannotAddPassenger",
       "The number of passengers for the reservation exceeds the room capacity");
    }
}
