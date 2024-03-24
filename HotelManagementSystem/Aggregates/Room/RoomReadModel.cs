namespace RestaurantManagementSystem.Aggregates;

public class RoomReadModel
{
    public required Guid Id { get; init; }
    public required Guid RoomId { get; init; }
    public required RoomCategory RoomCategory { get; init; }
    public required decimal Price { get; init; }
    public required bool isBooked { get; init; }
    public required bool isDeleted { get; init; }
}