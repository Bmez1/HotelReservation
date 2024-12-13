using MediatR;

namespace HotelReservation.Application.UseCases.Hotels.Create;
public record CreateCommand(
    string Name, 
    string Country,
    string City,
    long Phone,
    string? Description) : IRequest<Guid>;
