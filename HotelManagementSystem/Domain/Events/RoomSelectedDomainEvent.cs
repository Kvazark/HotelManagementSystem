using HotelManagementSystem.Domain.Common;

namespace HotelManagementSystem.Domain.Events;

public record RoomSelectedDomainEvent(Aggregates.Room Room) : IDomainEvent;