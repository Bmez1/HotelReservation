﻿using HotelReservation.Api.EndPoints.Reservations.Request;
using HotelReservation.Api.Extensions;
using HotelReservation.Api.HttpResponse;
using HotelReservation.Application.UseCases.Reservations.CreateReservation;
using HotelReservation.Application.UseCases.Reservations.GetReservationbyId;
using HotelReservation.Application.UseCases.Reservations.GetReservations;
using HotelReservation.Domain.Enums;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.Api.EndPoints.Reservations;

public static class MapReservation
{
    public static RouteGroupBuilder MapReservationsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetReservationsQuery());
            return result.ToHttpResponse();
        }).HasPermission(Permissions.GetReservations);

        endpoints.MapGet("/{id}", async (Guid id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetReservationByIdQuery(id));
            return result.ToHttpResponse();
        }).HasPermission(Permissions.GetReservations);

        endpoints.MapPost("/", async ([FromBody] CreateReservationRequest request, IMediator mediator) =>
        {
            var command = new CreateReservationCommand(
                request.HotelId,
                request.RoomId,
                request.CheckInDate,
                request.CheckOutDate,
                request.NumberOfGuests,
                request.EmergencyContactFullName,
                request.EmergencyContactPhoneNumber,
                request.Passengers.Select(x => new CreatePassengerCommand(
                    x.FullName,
                    x.DateOfBirth,
                    x.Gender,
                    x.DocumentType,
                    x.DocumentNumber,
                    x.Email,
                    x.PhoneNumber
                )).ToList());

            var result = await mediator.Send(command);
            return result.ToHttpResponse();
        }).HasPermission(Permissions.CreateReservation);

        return (RouteGroupBuilder)endpoints;
    }
}
