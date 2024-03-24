using RestaurantManagementSystem.Aggregates;

public class RoomId
{
    public Guid Value { get; }

    private RoomId(Guid value)
    {
        Value = value;
    }
    public static RoomId Of(Guid value)
    {
        if (value==Guid.Empty) throw new InvalidRoomIdException(value);
        return new RoomId(value);
    }

    public static implicit operator Guid(RoomId roomId)
    {
        return roomId.Value;
    }
}


public class PriceRoom
{
    public decimal  Value { get; }

    private PriceRoom(decimal  value)
    {
        Value = value;
    }
    public static PriceRoom Of(decimal  value)
    {
        if (value < 0) throw new InvalidPriceRoomException();
        return new PriceRoom(value);
    }
    public static implicit operator decimal (PriceRoom priceRoom)
    {
        return priceRoom.Value;
    }
}
