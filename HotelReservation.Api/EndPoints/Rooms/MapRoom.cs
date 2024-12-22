using HotelReservation.Api.EndPoints.Rooms.Request;
using HotelReservation.Api.Extensions;
using HotelReservation.Api.HttpResponse;
using HotelReservation.Application.UseCases.Rooms.AddRoom;
using HotelReservation.Application.UseCases.Rooms.ChangeStateRoom;
using HotelReservation.Application.UseCases.Rooms.GetRooms;
using HotelReservation.Application.UseCases.Rooms.UpdateRoom;
using HotelReservation.Domain.Enums;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.Api.EndPoints.Rooms;

public static class MapRoom
{
    public static RouteGroupBuilder MapRoomsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", async (IMediator mediator, Guid hotelId, bool all = false) =>
        {
            var result = await mediator.Send(new GetRoomsQuery(hotelId, all));
            return result.ToHttpResponse();
        }).HasPermission(Permissions.GetRooms); ;


        endpoints.MapPost("/", async ([FromBody] CreateRoomRequest request, Guid hotelId, IMediator mediator) =>
        {
            var result = await mediator.Send(new AddRoomCommand(
                hotelId,
                request.RoomNumber,
                request.BaseCost,
                request.Taxes,
                request.RoomType,
                request.BedCount,
                request.Capacity,
                request.Location
                ));

            return result.ToHttpResponse();
        }).HasPermission(Permissions.CreateRoom);

        endpoints.MapPut("/{roomId}", async (Guid hotelId, Guid roomId, [FromBody] UpdateRoomRequest request, IMediator mediator) =>
        {
            var result = await mediator.Send(new UpdateRoomCommand(
                hotelId,
                roomId,
                request.RoomNumber,
                request.BaseCost,
                request.Taxes,
                request.RoomType,
                request.BedCount,
                request.Capacity,
                request.Location
                ));

            return result.ToHttpResponse();
        }).HasPermission(Permissions.UpdateRoom);

        endpoints.MapPut("/State", async ([FromBody] ChangeStateRoomRequest request, Guid hotelId, IMediator mediator) =>
        {
            var result = await mediator.Send(new ChangeStateRoomCommand(
                hotelId,
                request.RoomId,
                request.Enable,
                request.ReasonDisable));

            return result.ToHttpResponse();
        }).HasPermission(Permissions.UpdateRoom);

        return (RouteGroupBuilder)endpoints;
    }
}
