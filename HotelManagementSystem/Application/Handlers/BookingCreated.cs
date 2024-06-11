using System.Text.Json;
using HotelManagementSystem.Application.EventBus;
using HotelManagementSystem.Domain.Events;
using HotelManagementSystem.DTO;
using MediatR;

namespace HotelManagementSystem.Application.Handlers;

public class BookingCreateDomainEventHandler : INotificationHandler<BookingCreateDomainEvent>
{
    private readonly KafkaProducerService _kafkaProducerService;
    public BookingCreateDomainEventHandler(KafkaProducerService kafkaProducerService)
    {
        _kafkaProducerService = kafkaProducerService;
    }
    public Task Handle(BookingCreateDomainEvent notification, CancellationToken cancellationToken)
    {
        var bookingMessage = new BookingRecordDto(Guid.NewGuid(), 
            "Confirmed", 
            new DateTimeOffset(notification.Booking.ReservationDates.ArrivalDate).ToUniversalTime().ToUnixTimeSeconds().ToString(), 
            new DateTimeOffset(notification.Booking.ReservationDates.DepartureDate).ToUniversalTime().ToUnixTimeSeconds().ToString(), 
            notification.Booking.NumberOfGuests.Value.ToString(),
            new DateTimeOffset(DateTime.UtcNow.Date.ToUniversalTime()).ToUnixTimeSeconds().ToString());

        Console.WriteLine($"Booking has been created {DateTime.Now}: " +
                          $"dates: {notification.Booking.ReservationDates.ArrivalDate.ToString("dd.MM.yyyy")}-{notification.Booking.ReservationDates.DepartureDate.ToString("dd.MM.yyyy")}, " +
                          $"number of guest: {notification.Booking.NumberOfGuests.Value}, " +
                          $"hotel: {notification.Booking.Hotel.Name.Value}, " +
                          $"hotel rating (0-5): {notification.Booking.Hotel.HotelStarRating.Value}, " +
                          $"number of room: {notification.Booking.Room.NumberRoom.Value}.");
        _kafkaProducerService.ProduceAsync("Booking", JsonSerializer.Serialize(bookingMessage));
        return Task.CompletedTask;
    }
}