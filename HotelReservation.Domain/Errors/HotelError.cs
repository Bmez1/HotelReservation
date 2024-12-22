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

        public static Error NotFoundByIdOrDisabled => Error.NotFound(
            "Hotel.NotFoundOrDisabled",
            "There is no hotel with the indicated ID or it is disabled.");

        public static Error NotAvailableForReservation => Error.Conflict(
        "Hotel.NotAvailableForReservation",
   "There is no availability to create the reservation. Please check the available hotels again.");

    }
}
