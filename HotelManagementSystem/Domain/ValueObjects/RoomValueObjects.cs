using HotelManagementSystem.Aggregates;
using HotelManagementSystem.Domain.Constants;


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
    public static Capacity Of(int value)
    {
        if (value <= 0) throw new InvalidCapacityException();
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
    // public static decimal GetCoefficientByCategory(string category)
    // {
    //     RoomCategories roomCategory;
    //     decimal coefficient = 1;
    //
    //     if (Enum.TryParse(category, out roomCategory))
    //     {
    //         switch (roomCategory)
    //         {
    //             case RoomCategories.Standard:
    //                 return 1.1m;
    //             case RoomCategories.JuniorSuite:
    //                 return 1.2m;
    //             case RoomCategories.Deluxe:
    //                 return 1.3m;
    //             case RoomCategories.Suite:
    //                 return 1.4m;
    //             default:
    //                 break;
    //         }
    //     }
    //     throw new InvalidRoomCategoryException();
    // }
    public static decimal GetCoefficientByCategory(string category)
    {
        
            switch (category)
            {
                case "Standard":
                    return 1.1m;
                case "JuniorSuite":
                    return 1.2m;
                case "Deluxe":
                    return 1.3m;
                case "Suite":
                    return 1.4m;
                default:
                    break;
            }
        
        throw new InvalidRoomCategoryException();
    }
}

public class RoomCategory
{
    public string  Value { get; }

    private RoomCategory(string  value)
    {
        Value = value;
    }
    public static RoomCategory Of(string  value)
    {

            //var category = (RoomCategories)Enum.Parse(typeof(RoomCategories), value);
            if(value != "Standard" && value != "JuniorSuite" && value != "Deluxe" && value != "Suite")  throw new InvalidRoomCategoryException();
            return new RoomCategory(value);
    }
    public static implicit operator string (RoomCategory roomCategory)
    {
        return roomCategory.Value;
    }
}
