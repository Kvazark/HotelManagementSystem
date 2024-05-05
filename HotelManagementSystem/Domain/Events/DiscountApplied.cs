using HotelManagementSystem.Aggregates;
using HotelManagementSystem.Domain.Common;

namespace HotelManagementSystem.Domain.Events;

public record DiscountApplied(Aggregates.Booking Booking): IDomainEvent;