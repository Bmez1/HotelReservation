﻿namespace HotelReservation.Application.Interfaces;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string body);
}
