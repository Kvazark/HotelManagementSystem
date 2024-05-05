using HotelManagementSystem.Aggregates;
using HotelManagementSystem.Domain.Common;

namespace HotelManagementSystem.Domain.Events;

public record RoomSelectedDomainEvents(Room Room):IDomainEvent;