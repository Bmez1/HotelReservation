using Common;

using MediatR;

namespace HotelReservation.Application.UseCases.Hotels.UpdateHotel;

public record UpdateHotelCommand(
    Guid HotelId,
    string Name,
    string Country,
    string City,
    long Phone,
    string? Description,
    bool Disable) : IRequest<Result<Guid>>;


