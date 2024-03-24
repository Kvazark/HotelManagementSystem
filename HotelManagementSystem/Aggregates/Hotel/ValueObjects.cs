using System.Runtime.InteropServices.JavaScript;

namespace RestaurantManagementSystem.Aggregates;

/// <summary>
/// Common
/// </summary>
public class Name
{
    public string Value { get; }

    private Name(string value)
    {
        Value = value;
    }
    public static Name Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new InvalidNameException();
        return new Name(value);
    }

    public static implicit operator string(Name name)
    {
        return name.Value;
    }
}

public class Address
{
    public string Value { get; }

    private Address(string value)
    {
        Value = value;
    }
    public static Address Of(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new InvalidAddressException();
        return new Address(value);
    }

    public static implicit operator string(Address address)
    {
        return address.Value;
    }
}

/// <summary>
/// Hotel
/// </summary>
public class HotelId
{
    public Guid Value { get; }

    private HotelId(Guid value)
    {
        Value = value;
    }
    public static HotelId Of(Guid value)
    {
        if (value==Guid.Empty) throw new InvalidHotelIdException(value);
        return new HotelId(value);
    }

    public static implicit operator Guid(HotelId hotelId)
    {
        return hotelId.Value;
    }
}

public class HotelStarRating
{
    public double  Value { get; }

    private HotelStarRating(double  value)
    {
        Value = value;
    }
    public static HotelStarRating Of(double  value)
    {
        if (value < 1 || value > 5) throw new InvalidHotelStarRatingException();
        return new HotelStarRating(value);
    }
    public static implicit operator double (HotelStarRating hotelStarRating)
    {
        return hotelStarRating.Value;
    }
}

public class RoomQuantity
{
    public RoomCategory Category { get; }
    public int Quantity { get; }

    private RoomQuantity(RoomCategory category, int quantity)
    {
        Category = category;
        Quantity = quantity;
    }
    public static RoomQuantity Of(RoomCategory category, int quantity)
    {
        if (quantity <= 0) throw new InvalidRoomQuantityException();
        return new RoomQuantity(category, quantity);
    }
}





