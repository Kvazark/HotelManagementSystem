namespace HotelManagementSystem.Aggregates;


public class InvalidHotelIdException : BadRequestException
{
    public InvalidHotelIdException(Guid hotelId)
        : base($"hotelId: '{hotelId}' is invalid.")
    {
    }
}

public class InvalidNameException : BadRequestException
{
    public InvalidNameException() : base("Name cannot be empty or whitespace.")
    {
    }
}
public class InvalidAddressException : BadRequestException
{
    public InvalidAddressException() : base("Address cannot be empty or whitespace.")
    {
    }
}

public class InvalidHotelStarRatingException : BadRequestException
{
    public InvalidHotelStarRatingException() : base("The value of the number of stars should be from 1 to 5.")
    {
    }
}
