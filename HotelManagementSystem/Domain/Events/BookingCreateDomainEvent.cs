using HotelManagementSystem.Domain.Common;
namespace HotelManagementSystem.Domain.Events;

public record BookingCreateDomainEvent(Aggregates.Booking Booking) : IDomainEvent;