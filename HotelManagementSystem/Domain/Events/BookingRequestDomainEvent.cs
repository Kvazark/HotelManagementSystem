using HotelManagementSystem.Domain.Common;

namespace HotelManagementSystem.Domain.Events;

public record BookingRequestDomainEvent(int NumberOfGuest, DateTime ArrivalDate, DateTime DepartureDate): IDomainEvent;