using HotelManagementSystem.Aggregates;


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

public class NumberRoom
{
    public string Value { get; }

    private NumberRoom(string value)
    {
        Value = value;
    }

    public static NumberRoom Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new InvalidNumberRoomException();
        return new NumberRoom(value);
    }

    public static implicit operator string(NumberRoom numberRoom)
    {
        return numberRoom.Value;
    }
}

public class Capacity
{
    public int  Value { get; }

    private Capacity(int  value)
    {
        Value = value;
    }
    public static Capacity Of(int  value)
    {
        if (value < 0) throw new InvalidCapacityException();
        return new Capacity(value);
    }
    public static implicit operator decimal (Capacity capacity)
    {
        return capacity.Value;
    }
}
public class RoomPrice
{
    public decimal  Value { get; }

    private RoomPrice(decimal  value)
    {
        Value = value;
    }
    public static RoomPrice Of(decimal  value)
    {
        if (value < 0) throw new InvalidRoomPriceException();
        return new RoomPrice(value);
    }
    public static implicit operator decimal (RoomPrice roomPrice)
    {
        return roomPrice.Value;
    }
}
