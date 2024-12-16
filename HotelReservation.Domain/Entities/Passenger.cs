using Common;

using HotelReservation.Domain.Enums;

namespace HotelReservation.Domain.Entities
{
    public class Passenger : EntityBase<Guid>
    {

        public string FullName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public Gender Gender { get; private set; }
        public DocumentType DocumentType { get; private set; }
        public string DocumentNumber { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }

        private Passenger(
            Guid id,
            string fullName,
            DateTime dateOfBirth,
            Gender gender,
            DocumentType documentType,
            string documentNumber,
            string email,
            string phoneNumber,
            DateTime createdAt)
        {
            Id = id;
            FullName = fullName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            DocumentType = documentType;
            DocumentNumber = documentNumber;
            Email = email;
            PhoneNumber = phoneNumber;
            CreatedAt = createdAt;
        }

        public static Passenger Create(
            string fullName,
            DateTime dateOfBirth,
            Gender gender,
            DocumentType documentType,
            string documentNumber,
            string email,
            string phoneNumber)
        {
            return new Passenger(
                Guid.NewGuid(),
                fullName,
                dateOfBirth,
                gender,
                documentType,
                documentNumber,
                email,
                phoneNumber,
                DateTime.UtcNow);
        }
    }
}
