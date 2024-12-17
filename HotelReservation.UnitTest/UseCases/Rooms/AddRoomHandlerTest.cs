using Bogus;
using HotelReservation.Application.Interfaces;
using HotelReservation.Application.UseCases.Rooms.AddRoom;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;
using HotelReservation.Domain.Errors;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Linq.Expressions;

namespace HotelReservation.UnitTest.UseCases.Rooms
{
    public class AddRoomHandlerTest
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomRepository _roomRepository;

        private readonly AddRoomHandler _addRoomHandler;

        public AddRoomHandlerTest()
        {
            _hotelRepository = Substitute.For<IHotelRepository>();
            _roomRepository = Substitute.For<IRoomRepository>();

            _addRoomHandler = new AddRoomHandler(_hotelRepository, _roomRepository);
        }

        [Fact]
        public async Task Should_Add_Room_Successfully()
        {
            // Arrange
            var hotel = new Faker<Hotel>()
                .CustomInstantiator(f => Hotel.Create(
                    f.Company.CompanyName(), f.Address.Country(), 532453453,
                    f.Address.City(), f.Lorem.Paragraph()
                )).Generate();

            var command = new Faker<AddRoomCommand>()
                .CustomInstantiator(f => new AddRoomCommand(
                    hotel.Id, f.Random.String2(5, 10), f.Random.Decimal(50, 200),
                    f.Random.Decimal(5, 20), f.PickRandom<RoomType>(), f.Random.Int(1, 4),
                    f.Random.Int(1, 4), f.Address.StreetName()
                )).Generate();

            _hotelRepository.GetByIdAsync(command.HotelId).Returns(hotel);
            _roomRepository.ExistsAsync(Arg.Any<Expression<Func<Room, bool>>>()).Returns(false);
            _roomRepository.AddAsync(Arg.Any<Room>()).Returns(Task.CompletedTask);
            _roomRepository.SaveChangesAsync().Returns(Task.CompletedTask);

            // Act
            var result = await _addRoomHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(command.HotelId, result.Value.HotelId);
            Assert.Equal(command.RoomNumber, result.Value.RoomNumber);
        }


        [Fact]
        public async Task Should_Fail_When_Hotel_Not_Found()
        {
            // Arrange
            var command = new Faker<AddRoomCommand>()
                .CustomInstantiator(f => new AddRoomCommand(
                    f.Random.Guid(), f.Random.String2(5, 10), f.Random.Decimal(50, 200),
                    f.Random.Decimal(5, 20), f.PickRandom<RoomType>(), f.Random.Int(1, 4),
                    f.Random.Int(1, 4), f.Address.StreetName()
                )).Generate();

            _hotelRepository.GetByIdAsync(command.HotelId).ReturnsNull();

            // Act
            var result = await _addRoomHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(HotelError.NotFoundById, result.Error);
        }


    }
}
