using RestaurantManagementSystem.Common;

namespace RestaurantManagementSystem.Aggregates;

public class Room  : Aggregate<RoomId>
{
    public RoomCategory RoomCategory { get; private set; } = default!;
    public PriceRoom Price { get; private set; } = default!;
}