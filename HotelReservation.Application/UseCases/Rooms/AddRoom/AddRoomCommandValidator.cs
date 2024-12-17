﻿using FluentValidation;

using HotelReservation.Domain.Enums;

namespace HotelReservation.Application.UseCases.Rooms.AddRoom
{
    public class AddRoomCommandValidator : AbstractValidator<AddRoomCommand>
    {
        public AddRoomCommandValidator()
        {
            RuleFor(x => x.HotelId).NotEmpty();
            RuleFor(x => x.RoomNumber).NotEmpty();
            RuleFor(x => x.BaseCost).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Taxes).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(x => x.Type).NotEmpty()
                .Must(value => Enum.IsDefined(typeof(RoomType), value))
                .WithMessage("Invalid room type.");
            RuleFor(x => x.Location).NotEmpty();
            RuleFor(x => x.Capacity).NotEmpty().GreaterThan(0);
            RuleFor(x => x.BedCount).NotEmpty().GreaterThan(0);
        }
    }
}