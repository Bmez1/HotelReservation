﻿using Bogus;
using HotelReservation.Application.Interfaces;
using HotelReservation.Application.UseCases.Hotels.Dtos;
using HotelReservation.Application.UseCases.Reservations.CreateReservation;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;
using HotelReservation.Domain.Errors;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace HotelReservation.UnitTest.UseCases.Reservations
{
    public class CreateReservationHandlerTest
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IPassengerRepository _passengerRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IEmailSender _emailSender;

        private readonly CreateReservationHandler _createReservationHandler;

        public CreateReservationHandlerTest()
        {
            _hotelRepository = Substitute.For<IHotelRepository>();
            _passengerRepository = Substitute.For<IPassengerRepository>();
            _reservationRepository = Substitute.For<IReservationRepository>();
            _emailSender = Substitute.For<IEmailSender>();

            _createReservationHandler = new CreateReservationHandler(
                _hotelRepository,
                _passengerRepository,
                _reservationRepository,
                _emailSender
            );
        }

        [Fact]
        public async Task Should_Fail_When_Traveler_Not_Found()
        {
            // Arrange
            var command = new Faker<CreateReservationCommand>()
                .CustomInstantiator(f => new CreateReservationCommand(
                    f.Random.Guid(), f.Random.Guid(), f.Random.Guid(), f.Address.City(),
                    f.Date.Future(), f.Date.Future().AddDays(5), f.Random.Int(1, 5),
                    f.Name.FullName(), f.Phone.PhoneNumber()
                )).Generate();

            _passengerRepository.GetByIdAsync(command.TravelerId).ReturnsNull();

            // Act
            var result = await _createReservationHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(PassengerError.NotFoundById, result.Error);
        }

        [Fact]
        public async Task Should_Fail_When_No_Hotels_Available()
        {
            // Arrange
            var command = new Faker<CreateReservationCommand>()
                .CustomInstantiator(f => new CreateReservationCommand(
                    f.Random.Guid(), f.Random.Guid(), f.Random.Guid(), f.Address.City(),
                    f.Date.Future(), f.Date.Future().AddDays(5), f.Random.Int(1, 5),
                    f.Name.FullName(), f.Phone.PhoneNumber()
                )).Generate();

            var traveler = new Faker<Passenger>()
                .CustomInstantiator(f => Passenger.Create(
                    f.Name.FullName(), f.Date.Past(30), f.PickRandom<Gender>(),
                    f.PickRandom<DocumentType>(), f.Random.AlphaNumeric(10),
                    f.Internet.Email(), f.Phone.PhoneNumber()
                )).Generate();

            _passengerRepository.GetByIdAsync(command.TravelerId).Returns(traveler);
            _hotelRepository.GetHotelsForReservationAsync(command.DestinationCity, command.CheckInDate, command.CheckOutDate, command.NumberOfGuests)
                .Returns([]);

            // Act
            var result = await _createReservationHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(HotelError.NotAvailableForReservation, result.Error);
        }


        [Fact]
        public async Task Should_Create_Reservation_Successfully()
        {
            // Arrange
            var command = new Faker<CreateReservationCommand>()
                .CustomInstantiator(f => new CreateReservationCommand(
                    f.Random.Guid(), f.Random.Guid(), f.Random.Guid(), f.Address.City(),
                    f.Date.Future(), f.Date.Future().AddDays(5), f.Random.Int(1, 5),
                    f.Name.FullName(), f.Phone.PhoneNumber()
                )).Generate();

            var traveler = new Faker<Passenger>()
                .CustomInstantiator(f => Passenger.Create(
                    f.Name.FullName(), f.Date.Past(30), f.PickRandom<Gender>(),
                    f.PickRandom<DocumentType>(), f.Random.AlphaNumeric(10),
                    f.Internet.Email(), f.Phone.PhoneNumber()
                )).Generate();

            var hotelResponse = new Faker<HotelsForReservationResponseDto>()
                .RuleFor(h => h.HotelId, f => command.HotelId)
                .RuleFor(h => h.RoomId, f => command.RoomId)
                .RuleFor(h => h.HotelName, f => f.Company.CompanyName())
                .RuleFor(h => h.City, f => f.Address.Country())
                .RuleFor(h => h.Phone, f => 1231242)
                .RuleFor(h => h.City, f => f.Address.City())
                .RuleFor(h => h.BaseCost, f => f.Random.Decimal(50, 200))
                .RuleFor(h => h.Taxes, f => f.Random.Decimal(1, 5))
                .RuleFor(h => h.Type, f => RoomType.Single)
                .Generate();

            _passengerRepository.GetByIdAsync(command.TravelerId).Returns(traveler);
            _hotelRepository.GetHotelsForReservationAsync(command.DestinationCity, command.CheckInDate, command.CheckOutDate, command.NumberOfGuests)
                .Returns([hotelResponse]);
            _reservationRepository.AddAsync(Arg.Any<Reservation>()).Returns(Task.CompletedTask);
            _reservationRepository.SaveChangesAsync().Returns(Task.CompletedTask);

            // Act
            var result = await _createReservationHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Contains("Reservation created with code:", result.Value);
        }
    }
}
