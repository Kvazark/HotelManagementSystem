using System.Text.Json;
using HotelManagementSystem.Application.EventBus;
using HotelManagementSystem.Domain.Events;
using HotelManagementSystem.DTO;
using MediatR;

namespace HotelManagementSystem.Application.Handlers;

public class RoomSelectedDomainEventHandler : INotificationHandler<RoomSelectedDomainEvent>
{
    private readonly KafkaProducerService _kafkaProducerService;

    public RoomSelectedDomainEventHandler(KafkaProducerService kafkaProducerService)
    {
        _kafkaProducerService = kafkaProducerService;
    }

    public async Task Handle(RoomSelectedDomainEvent notification, CancellationToken cancellationToken)
    {
        var roomSelectedMessage = new RoomSelectedEventDto(Guid.NewGuid(), 
            notification.Room.NumberRoom, 
            notification.Room.RoomCategory, 
            notification.Room.Hotel.Name, 
            new DateTimeOffset(DateTime.UtcNow.Date.ToUniversalTime()).ToUnixTimeSeconds().ToString());
        
        await _kafkaProducerService.ProduceAsync("RoomSelected", JsonSerializer.Serialize(roomSelectedMessage));
    }
}