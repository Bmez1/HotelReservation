using Common;

using MediatR;

namespace HotelReservation.Application.UseCases.Hotels.CreateHotel;
public record CreateHotelCommand(
    string Name,
    string Country,
    string City,
    long Phone,
    string? Description) : IRequest<Result<Guid>>;
