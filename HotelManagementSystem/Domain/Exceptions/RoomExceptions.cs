namespace HotelManagementSystem.Aggregates;

public class InvalidRoomIdException : BadRequestException
{
    public InvalidRoomIdException(Guid roomId)
        : base($"roomId: '{roomId}' is invalid.")
    {
    }
}

public class InvalidCapacityException : BadRequestException
{
    public InvalidCapacityException() : base("The capacity cannot be negative or equals 0.")
    {
    }
}

public class InvalidNumberRoomException : BadRequestException
{
    public InvalidNumberRoomException() : base("The number room cannot be empty or whitespace")
    {
    }
}

public class InvalidRoomPriceException : BadRequestException
{
    public InvalidRoomPriceException() : base("The price of room cannot be negative.")
    {
    }
}

public class InvalidRoomCategoryException : BadRequestException
{
    public InvalidRoomCategoryException() : base("There is no such category.")
    {
    }
}

