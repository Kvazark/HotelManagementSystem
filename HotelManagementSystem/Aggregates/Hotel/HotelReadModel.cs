namespace RestaurantManagementSystem.Aggregates;

public class HotelReadModel
{
    public required Guid Id { get; init; }
    public required Guid HotelId { get; init; }
    public required string Name { get; init; }
    public required string Address { get; init; }
    public double HotelStarRating { get; init; }
    public RoomQuantity RoomQuantity { get; init; }
    public required bool isDeleted { get; init; }
}