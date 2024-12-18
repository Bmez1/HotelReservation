using Bogus;
using HotelReservation.Application.Interfaces;
using HotelReservation.Application.UseCases.Hotels.UpdateHotel;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Errors;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace HotelReservation.UnitTest.UseCases.Hotels
{
    public class UpdateHotelHandlerTest
    {
        private readonly IHotelRepository _hotelRepository;

        private readonly UpdateHotelHandler _updateHotelHandler;

        public UpdateHotelHandlerTest()
        {
            _hotelRepository = Substitute.For<IHotelRepository>();

            _updateHotelHandler = new UpdateHotelHandler(_hotelRepository);
        }

        [Fact]
        public async Task Should_Update_Hotel_Successfully()
        {
            // Arrange
            var existingHotel = new Faker<Hotel>()
                .CustomInstantiator(f => Hotel.Create(
                    f.Company.CompanyName(), f.Address.Country(), 5435234,
                    f.Address.City(), f.Lorem.Paragraph()
                )).Generate();

            var command = new Faker<UpdateHotelCommand>()
                .CustomInstantiator(f => new UpdateHotelCommand(
                    existingHotel.Id, f.Company.CompanyName(), f.Address.Country(),
                    f.Address.City(), 4624523, f.Lorem.Paragraph(),
                    false
                )).Generate();

            _hotelRepository.GetByIdAsync(command.HotelId, true).Returns(existingHotel);
            _hotelRepository.SaveChangesAsync().Returns(Task.CompletedTask);

            // Act
            var result = await _updateHotelHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(command.HotelId, result.Value);
        }

        [Fact]
        public async Task Should_Fail_When_Hotel_Not_Found()
        {
            // Arrange
            var command = new Faker<UpdateHotelCommand>()
                .CustomInstantiator(f => new UpdateHotelCommand(
                    f.Random.Guid(), f.Company.CompanyName(), f.Address.Country(),
                    f.Address.City(), 3452344, f.Lorem.Paragraph(),
                    false
                )).Generate();

            _hotelRepository.GetByIdAsync(command.HotelId).ReturnsNull();

            // Act
            var result = await _updateHotelHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal(HotelError.NotFoundById, result.Error);
        }
    }
}
