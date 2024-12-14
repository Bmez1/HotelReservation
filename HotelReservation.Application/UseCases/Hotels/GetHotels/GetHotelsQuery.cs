using Common;

using HotelReservation.Application.UseCases.Hotels.Dtos;

using MediatR;

namespace HotelReservation.Application.UseCases.Hotels.GetHotels;

public record class GetHotelsQuery(bool All) : IRequest<Result<IEnumerable<HotelResponseDto>>>;
