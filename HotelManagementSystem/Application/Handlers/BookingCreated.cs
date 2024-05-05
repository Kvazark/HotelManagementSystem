using HotelManagementSystem.Domain.Events;
using MediatR;

namespace HotelManagementSystem.Application.Handlers;

public class BookingCreateDomainEventHandler : INotificationHandler<BookingCreateDomainEvent>
{
    public BookingCreateDomainEventHandler(){}
    public Task Handle(BookingCreateDomainEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"A reservation has been created {DateTime.Now}: " +
                          $"dates: {notification.Booking.ReservationDates.ArrivalDate}-{notification.Booking.ReservationDates.DepartureDate}, " +
                          $"number of guest: {notification.Booking.NumberOfGuests}," +
                          $"hotel: {notification.Booking.Hotel.Name}," +
                          $"number og room: {notification.Booking.Room.NumberRoom}.");
        return Task.CompletedTask;
    }
}