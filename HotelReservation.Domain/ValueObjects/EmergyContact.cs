namespace HotelReservation.Domain.ValueObjects
{
    public class EmergencyContact
    {
        public string FullName { get; private set; }
        public string PhoneNumber { get; private set; }

        public EmergencyContact(string fullName, string phoneNumber)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
        }
    }
}
