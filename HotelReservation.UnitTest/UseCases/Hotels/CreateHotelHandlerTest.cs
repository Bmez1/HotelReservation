using Bogus;
using HotelReservation.Application.Interfaces;
using HotelReservation.Application.UseCases.Hotels.CreateHotel;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Errors;
using NSubstitute;
using System.Linq.Expressions;

namespace HotelReservation.UnitTest.UseCases.Hotels
{
    public class CreateHotelHandlerTest
    {
        private readonly IHotelRepository _hotelRepository;

        private readonly CreateHotelHandler _createHotelHandler;

        public CreateHotelHandlerTest()
        {
            _hotelRepository = Substitute.For<IHotelRepository>();

            _createHotelHandler = new CreateHotelHandler(_hotelRepository);
        }

        [Fact]
        public async Task Should_Create_Hotel_Successfully()
        {
            // Arrange
            var command = new Faker<CreateHotelCommand>()
                .CustomInstantiator(f => new CreateHotelCommand(
                    f.Company.CompanyName(), f.Address.Country(), f.Address.City(),
                    34234234, f.Lorem.Paragraph()
                )).Generate();

            _hotelRepository.ExistsAsync(Arg.Any<Expression<Func<Hotel, bool>>>()).Returns(false);
            _hotelRepository.AddAsync(Arg.Any<Hotel>()).Returns(Task.CompletedTask);
            _hotelRepository.SaveChangesAsync().Returns(Task.CompletedTask);

            // Act
            var result = await _createHotelHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotEqual(Guid.Empty, result.Value);
        }

        [Fact]
        public async Task Should_Fail_When_Hotel_Already_Exists()
        {
            // Arrange
            var command = new Faker<CreateHotelCommand>()
                .CustomInstantiator(f => new CreateHotelCommand(
                    f.Company.CompanyName(), f.Address.Country(), f.Address.City(),
                    457456745, f.Lorem.Paragraph()
                )).Generate();

            _hotelRepository.ExistsAsync(Arg.Any<Expression<Func<Hotel, bool>>>()).Returns(true);

            // Act
            var result = await _createHotelHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(HotelError.AlreadyExists, result.Error);
        }
    }
}
