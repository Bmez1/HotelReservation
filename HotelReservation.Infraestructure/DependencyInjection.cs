using HotelReservation.Application.Interfaces;
using HotelReservation.Infraestructure.DataBase;
using HotelReservation.Infraestructure.Repositories;
using HotelReservation.Infraestructure.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelReservation.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) =>
        services
        .AddServices()
        .AddDatabase(configuration);

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<IPassengerRepository, PassengerRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IEmailSender, EmailSender>();
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(
            options => options
                .UseSqlServer(connectionString));

        return services;
    }
}
