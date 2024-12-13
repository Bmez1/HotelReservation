using HotelReservation.Application.Interfaces;
using HotelReservation.Domain.Entities;
using MediatR;

namespace HotelReservation.Application.UseCases.Hotels.Create;

public class CreateHandler(IHotelRepository hotelRepository) : IRequestHandler<CreateCommand, Guid>
{
    public async Task<Guid> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var hotel = Hotel.Create(
            request.Name,
            request.Country,
            request.Phone,
            request.City,
            request.Description
            );

        await hotelRepository.AddAsync(hotel);
        await hotelRepository.SaveChangesAsync();
        return hotel.Id;
    }
}
