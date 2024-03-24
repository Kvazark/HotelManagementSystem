namespace RestaurantManagementSystem.Aggregates;

public class OrderReadModel
{
    public required Guid Id { get; init; }
    public required Guid OrderId { get; init; }
    public required ReservationDates ReservationDates { get; init; }
    public required ServicesList Services { get; init; }
    public required int NumberOfGuests { get; init; }
    public required decimal OrderPrice { get; init; }
    public required bool isDeleted { get; init; }
}