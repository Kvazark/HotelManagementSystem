namespace HotelManagementSystem.Domain.Constants;

public class RatioRoomCategories
{
    public static decimal GetCoefficientByCategory(RoomCategories category)
    {
        decimal coefficient = 1;

        switch (category)
        {
            case RoomCategories.Standard:
                coefficient = 1.1m;  
                break;
            case RoomCategories.JuniorSuite:
                coefficient = 1.2m; 
                break;
            case RoomCategories.Deluxe:
                coefficient = 1.3m;  
                break;
            case RoomCategories.Suite:
                coefficient = 1.4m;  
                break;
            default:
                break;
        }
        return coefficient;
    }
}