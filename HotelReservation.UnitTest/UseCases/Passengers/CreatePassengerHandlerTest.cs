using Bogus;
using HotelReservation.Application.Interfaces;
using HotelReservation.Application.UseCases.Passengers.CreatePassenger;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;
using HotelReservation.Domain.Errors;
using HotelReservation.Domain.ValueObjects;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Linq.Expressions;

namespace HotelReservation.UnitTest.UseCases.Passengers
{
    public class CreatePassengerHandlerTest
    {
        private readonly IPassengerRepository _passengerRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly CreatePassengerHandler _passengerHandler;

        public CreatePassengerHandlerTest()
        {
            _passengerRepository = Substitute.For<IPassengerRepository>();
            _reservationRepository = Substitute.For<IReservationRepository>();
            _passengerHandler = new CreatePassengerHandler(_passengerRepository, _reservationRepository);
        }

        [Fact]
        public async Task Should_Create_Passenger()
        {
            // Arrange
            var command = new Faker<CreatePassengerCommand>()
            .CustomInstantiator(f => new CreatePassengerCommand(
                f.Random.Guid(),
                f.Name.FullName(),
                f.Date.Past(30),
                f.PickRandom<Gender>(),
                f.PickRandom<DocumentType>(),
                f.Random.AlphaNumeric(10),
                f.Internet.Email(),
                f.Phone.PhoneNumber()
            )).Generate();

            var reservation = new Faker<Reservation>()
                .CustomInstantiator(f => Reservation.Create(
                    f.Random.Guid(),
                    f.Random.Guid(),
                    f.Random.Guid(),
                    f.Date.Future(1),
                    f.Date.Future(1),
                    ReservationStatus.Active,
                    3,
                    new EmergencyContact(f.Name.FullName(), f.Phone.PhoneNumber())
                    ))
                .Generate();

            _reservationRepository.GetByIdAsync(command.ReservationId!.Value, true).Returns(reservation);
            _passengerRepository.ListAsync(Arg.Any<Expression<Func<Passenger, bool>>>(), Arg.Any<bool>()).Returns([]);
            _passengerRepository.AddAsync(Arg.Any<Passenger>()).Returns(Task.CompletedTask);
            _passengerRepository.SaveChangesAsync().Returns(Task.CompletedTask);

            // Act
            var result = await _passengerHandler.Handle(command, CancellationToken.None);

            // Assert
            await _passengerRepository.Received().AddAsync(Arg.Any<Passenger>());
            await _passengerRepository.Received().SaveChangesAsync();

            Assert.NotNull(result);
            Assert.IsType<Guid>(result.Value.Id);
            Assert.Equal(command.FullName, result.Value.FullName);
        }

        [Fact]
        public async Task Should_Fail_When_Reservation_Not_Found()
        {
            // Arrange
            var command = new Faker<CreatePassengerCommand>()
                .CustomInstantiator(f => new CreatePassengerCommand(
                    f.Random.Guid(), f.Name.FullName(), f.Date.Past(30), f.PickRandom<Gender>(),
                    f.PickRandom<DocumentType>(), f.Random.AlphaNumeric(10), f.Internet.Email(), f.Phone.PhoneNumber()
                )).Generate();

            _reservationRepository.GetByIdAsync(command.ReservationId!.Value, true).ReturnsNull();

            // Act
            var result = await _passengerHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(ReservationError.NotFoundById, result.Error);
        }

        [Fact]
        public async Task Should_Fail_When_Reservation_Cannot_Add_Passenger()
        {
            // Arrange
            var command = new Faker<CreatePassengerCommand>()
                .CustomInstantiator(f => new CreatePassengerCommand(
                    f.Random.Guid(), f.Name.FullName(), f.Date.Past(30), f.PickRandom<Gender>(),
                    f.PickRandom<DocumentType>(), f.Random.AlphaNumeric(10), f.Internet.Email(), f.Phone.PhoneNumber()
                )).Generate();

            var reservation = new Faker<Reservation>()
                .CustomInstantiator(f => Reservation.Create(
                    f.Random.Guid(), f.Random.Guid(), f.Random.Guid(), f.Date.Future(1), f.Date.Future(1),
                    ReservationStatus.Active, 3, new EmergencyContact(f.Name.FullName(), f.Phone.PhoneNumber()), 3
                )).Generate();

            _reservationRepository.GetByIdAsync(command.ReservationId!.Value, true).Returns(reservation);

            // Act
            var result = await _passengerHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(ReservationError.CannotAddPassenger, result.Error);
        }


    }
}
