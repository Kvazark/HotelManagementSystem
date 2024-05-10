using HotelManagementSystem.Domain.Events;
using MediatR;

namespace HotelManagementSystem.Application.Handlers;

public class BookingCreateDomainEventHandler : INotificationHandler<BookingCreateDomainEvent>
{
    public BookingCreateDomainEventHandler(){}
    public Task Handle(BookingCreateDomainEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Booking has been created {DateTime.Now}: " +
                          $"dates: {notification.Booking.ReservationDates.ArrivalDate.ToString("dd.MM.yyyy")}-{notification.Booking.ReservationDates.DepartureDate.ToString("dd.MM.yyyy")}, " +
                          $"number of guest: {notification.Booking.NumberOfGuests.Value}, " +
                          $"hotel: {notification.Booking.Hotel.Name.Value}, " +
                          $"hotel rating (0-5): {notification.Booking.Hotel.HotelStarRating.Value}, " +
                          $"number of room: {notification.Booking.Room.NumberRoom.Value}.");
        return Task.CompletedTask;
    }
}