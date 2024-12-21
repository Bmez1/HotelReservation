using HotelReservation.Api.EndPoints.Users.Request;
using HotelReservation.Api.Extensions;
using HotelReservation.Api.HttpResponse;
using HotelReservation.Application.UseCases.Users.GetById;
using HotelReservation.Application.UseCases.Users.Login;
using HotelReservation.Application.UseCases.Users.Register;
using HotelReservation.Domain.Enums;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.Api.EndPoints.Users;

public static class MapUser
{
    public static RouteGroupBuilder MapUsersEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/{userId:guid}", async (IMediator mediator, Guid userId) =>
        {
            var result = await mediator.Send(new GetUserByIdQuery(userId));
            return result.ToHttpResponse();
        })
        .RequireAuthorization()
        .HasPermission(Permissions.GetUsers);


        endpoints.MapPost("/", async ([FromBody] RegisterUserRequest request, IMediator mediator) =>
        {
            var result = await mediator.Send(new RegisterUserCommand(
                request.UserName,
                request.Email,
                request.FirstName,
                request.LastName,
                request.Password
                ));

            return result.ToHttpResponse();
        })
          .RequireAuthorization()
          .HasPermission(Permissions.CreateUser);

        endpoints.MapPost("/login", async ([FromBody] LoginRequest request, IMediator mediator) =>
        {
            var result = await mediator.Send(new LoginUserCommand(
                request.UserName,
                request.Password
                ));

            return result.ToHttpResponse();
        });

        return (RouteGroupBuilder)endpoints;
    }
}
