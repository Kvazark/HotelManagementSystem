namespace RestaurantManagementSystem.Aggregates;

public class InvalidRoomIdException : BadRequestException
{
    public InvalidRoomIdException(Guid roomId)
        : base($"roomId: '{roomId}' is invalid.")
    {
    }
}

public class InvalidPriceRoomException : BadRequestException
{
    public InvalidPriceRoomException() : base("The price of room cannot be negative.")
    {
    }
}
